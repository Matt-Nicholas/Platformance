using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public enum GameMode
    {
        None,
        SinglePlayer,
        CoOp,
        Competetive
    }
    public static Game Instance { get; private set; }
    public GameSettings GameSettings;
    public AudioManager AudioManager;
    public Action<int> OnPlayerJoinedGame;
    public Action<int> OnStageCompleted;
    
    [HideInInspector] public GameMode currentMode;
    [HideInInspector] public List<Player> Players = new();
    
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private InputSystemUIInputModule _uiInputModule;
    
    [SerializeField] private Transform _playersContainer;

    private Tournament _tournament;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void StartTournament()
    {
        _tournament = new Tournament(2);
        _tournament.StartTournament();
    }

    public void StartGame()
    {
        StartTournament();
    }

    [UsedImplicitly]
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.SetParent(_playersContainer);
        
        var player = playerInput.GetComponent<Player>();
        var index = Players.Count;
        var inputHandler = playerInput.GetComponent<PlayerInputHandler>();
        inputHandler.device = playerInput.devices[0];
        inputHandler.playerIndex = index;
        Players.Add(player);
        
        OnPlayerJoinedGame?.Invoke(index);
    }

    private void Start()
    {
        _playerInputManager.DisableJoining();
        currentMode = GameMode.None;
        
#if !UNITY_EDITOR
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
        // The PlayerOneAssign scene is used to map the Player One input device
        SceneManager.LoadScene("PlayerOneAssign");
        
    }

    public void SetJoiningEnabled(bool enabled)
    {
        if (enabled)
            _playerInputManager.EnableJoining();
        else
            _playerInputManager.DisableJoining();
    }

    public void LoadMainMenu()
    {
        for (int i = 1; i < Players.Count; i++)
        {
            Destroy(Players[i]);
        }
        Players[0].SetupForUI();
        SceneManager.LoadScene("MainMenu");
    }

    public void TournamentCompleted()
    {
        _tournament = null;
        LoadMainMenu();
    }
    
    public void LoadPlayerSetup(GameMode mode)
    {
        if (mode == GameMode.SinglePlayer)
        {
            currentMode = mode;
            SceneManager.LoadScene("CharacterSelector");
        }
        else
        {
            SceneManager.LoadScene("MultiplayerSetup");
        }
    }

    public void ReportStageCompleted()
    {
        OnStageCompleted?.Invoke(0);
    }
}
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
    public AudioManager AudioManager;
    public Action<int> OnPlayerJoinedGame;
    [HideInInspector] public GameMode currentMode;
    [HideInInspector] public List<Player> Players = new();
    
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private InputSystemUIInputModule _uiInputModule;
    
    [SerializeField] private Transform _playersContainer;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prototyping");
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
        SceneManager.LoadScene("MainMenu");
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
}
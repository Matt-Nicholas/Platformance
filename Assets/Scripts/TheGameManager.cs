using Dafunk;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheGameManager : MonoBehaviour
{
    public Dictionary<int, Player> Players = new ();
    public AudioManager AudioManager;
    public static TheGameManager Instance => _instance;
    
    private static TheGameManager _instance;

    public enum GameMode
    {
        None, SinglePlayer, CoOp, Competetive
    }

    public GameMode currentMode;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
        // The PlayerAssign scene is used to map the Player One input device
        SceneManager.LoadScene("PlayerAssign");
        currentMode = GameMode.None;
    }

    public void LoadPlayerSetup(GameMode mode)
    {
        if (mode == GameMode.SinglePlayer)
        {
            currentMode = mode;
            SceneManager.LoadScene("SinglePlayerSetup");
        }
        else
        {
            SceneManager.LoadScene("MultiplayerSetup");
        }
    }

    public void CreatePlayer(int controllerID, bool loadMainMenu)
    {
        Players[1] = new Player(1, controllerID);
        Players[2] = new Player(2, (controllerID == 1) ? 2 : 1);

        if (loadMainMenu) SceneManager.LoadScene("MainMenu");
    }
}


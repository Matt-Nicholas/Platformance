using Dafunk;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheGameManager : MonoBehaviour
{
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    [HideInInspector]
    public AudioManager audioManager;

    public static TheGameManager Instance { get { return _instance; } }

    private static TheGameManager _instance;

    public enum GameMode
    {
        None, SinglePlayer, CoOp, Competetive
    }

    public GameMode currentMode;

    public TheGameManager() { }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        InitRequiredComponents();
    }

    private void Start()
    {
        Cursor.visible = false;

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

    private void InitRequiredComponents()
    {
        audioManager = GetComponent<AudioManager>();
        if (audioManager == null)
        {
            audioManager = gameObject.AddComponent<AudioManager>();
        }
    }



    public void CreatePlayer(int controllerID, bool loadMainMenu)
    {

        players[1] = new Player(1, controllerID);
        players[2] = new Player(2, (controllerID == 1) ? 2 : 1);

        if (loadMainMenu) SceneManager.LoadScene("MainMenu");
    }
}


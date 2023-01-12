using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _firstSelected;
    private readonly Game _game = Game.Instance;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelected);
    }

    public void SinglePlayerSelected()
    {
        _game.LoadPlayerSetup(Game.GameMode.SinglePlayer);
    }

    public void QuitSelected()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    TheGameManager GM = TheGameManager.Instance;

    public void SinglePlayerSelected()
    {
        GM.LoadPlayerSetup(TheGameManager.GameMode.SinglePlayer);
    }

    public void VersusSelected()
    {
        GM.LoadPlayerSetup(TheGameManager.GameMode.None);
    }

    public void QuitSelected()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

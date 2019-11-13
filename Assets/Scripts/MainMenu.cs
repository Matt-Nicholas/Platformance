using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

  TheGameManager GM = TheGameManager.Instance;

  public void SinglePlayerSelected() {
    GM.LoadPlayerSetup(TheGameManager.GameMode.SinglePlayer);
  }

  public void MultiplayerSelected() {
    GM.LoadPlayerSetup(TheGameManager.GameMode.None);
  }

  public void QuitSelected() {

  }
}

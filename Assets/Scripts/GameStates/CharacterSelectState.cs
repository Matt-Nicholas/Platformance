using Dafunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CharacterSelectState:IState {
  public CharacterSelectState() {

  }

  public void Enter() {
    SceneManager.LoadScene(2);
  }

  public void Execute() {
    if(InputManager.AButtonDown()) {
      //TheGameManager.Instance.ChangeState(TheGameManager.Instance.);
    }
  }

  public void Exit() {

  }
}

using Dafunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenuState:IState {
  //Firework fireworks;

  public MainMenuState() {

  }

  public void Enter() {
    //fireworks = 
    SceneManager.LoadScene(1);
  }

  public void Execute() {
    //if(DateTime.Now.Second % 3 == 0) {

    //}
  }

  public void Exit() {

  }

  private void SpawnWaves() {
    System.Random r = new System.Random();
    Vector3 position = new Vector3((float)(r.NextDouble() * 20) - 10, 0, (float)(r.NextDouble() * 20) - 10);
    //Instantiate(prefab, position, Quaternion.identity);

  }
  }

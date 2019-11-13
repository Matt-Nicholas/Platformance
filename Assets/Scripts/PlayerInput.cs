using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput:MonoBehaviour {

  
  public int playerNumber;
  public int playerID;

  PlayerController player;

  void Start() {
    player = GetComponent<PlayerController>();
    if(transform.position.x < 15) {
      playerNumber = 1;
      playerID = TheGameManager.Instance.players[1].controllerID;
    }
    else {
      playerNumber = 2;
      playerID = TheGameManager.Instance.players[2].controllerID;
    }

    transform.GetComponent<Renderer>().material.color = TheGameManager.Instance.players[playerNumber].Color;
  }

  void Update() {
    player.SetDirectionalInput(new Vector2(InputManager.MainHorizontal(playerID), InputManager.MainVertical(playerID)), InputManager.RightBumper(playerID));

    if(InputManager.AButtonDown(playerID)) { 
      player.OnJumpInputDown();
    }
    if(InputManager.AButtonUp(playerID)) {
      player.OnJumpInputUp();
    }
  }
}

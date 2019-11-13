using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

  public int playerID;
  public int controllerID;
  private Color color;

  public Player(int playerID, int controllerID) {
    this.playerID = playerID;
    this.controllerID = controllerID;
  }

  public Color Color {
    get {
      return color;
    }

    set {
      color = value;
    }
  }
}

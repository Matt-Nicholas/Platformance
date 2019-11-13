using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UncolorAfterTimeBlock:Block {

  [SerializeField] private float countTime = 0;
  private float counter;

  public override void Start() {
    base.Start();
   if(countTime == 0) {
      print(this.name + " must have an timer greater than 0");
    }
  }

  private void Update() {
    if(counter >= 0) {
      counter -= Time.deltaTime;
    }
    if(counter <= 0) {
      SetColor(GameplayManager.startColor);
    }
  }

  private void Entered(Color playerColor) { 
    SetColor(playerColor);
    counter = countTime;

  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSequenceBlock:Block {

  [SerializeField] private float countdownTime;
  private int orderNumber;
  private TimedSequenceBlock[] sequenceBlocks;
  private float timer = 0f;
  private bool isTriggered = false;

  public override void Start() {
    base.Start();
    sequenceBlocks = FindObjectsOfType(typeof(TimedSequenceBlock)) as TimedSequenceBlock[];
    string text = GetComponentInChildren<TextMesh>().text;
    orderNumber = Convert.ToInt32(text.Trim());
  }

  private void Update() {
    
    if(timer <= 0) {
      if(isTriggered) {

        SetColor(GameplayManager.startColor);
        isTriggered = false;
      }
    }
    else {
      if(!NextBlockIsColored()) {
        timer -= Time.deltaTime;
      }
      else {
        timer = countdownTime;
      }
    }
  }

  private void Entered(Color playerColor) {
    if(PrevousBlockIsColored()) {
      isTriggered = true;
      SetColor(playerColor);
      timer = countdownTime;
    }
  }

  private bool PrevousBlockIsColored() {
    if(this.orderNumber == 1) return true;
    for(int i = 0; i < sequenceBlocks.Length; i++) {
      if(sequenceBlocks[i].orderNumber == (this.orderNumber - 1)) {
        if(sequenceBlocks[i].rend.material.color != GameplayManager.startColor) {
          return true;
        }
      }
    }
    return false;
  }
  
  private bool NextBlockIsColored() {
    // If this is the last block in the sequence return false
    if(this.orderNumber == sequenceBlocks.Length) return false;
    for(int i = 0; i < sequenceBlocks.Length; i++) {
      if(sequenceBlocks[i].orderNumber == (this.orderNumber + 1)) {
        if(sequenceBlocks[i].rend.material.color != GameplayManager.startColor) {
          return true;
        }
      }
    }
    return false;
  }
}

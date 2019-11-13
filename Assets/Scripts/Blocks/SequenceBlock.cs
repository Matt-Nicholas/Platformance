using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceBlock : Block {

  SequenceBlock[] sequenceBlocks;

  public int orderNumber;

  public override void Start() {
    base.Start();
    //SetColor(GamePlayState.startColor);
    sequenceBlocks = FindObjectsOfType(typeof(SequenceBlock)) as SequenceBlock[];
    string text = GetComponentInChildren<TextMesh>().text;
    orderNumber = Convert.ToInt32(text.Trim());
  }

  private void Entered(Color playerColor) {
    if(PrevousBlockIsColored()) {
      SetColor(playerColor);
    }
  }

  private bool PrevousBlockIsColored() {
    if(this.orderNumber == 1) return true;

    for(int i = 0; i < sequenceBlocks.Length; i++) {
      if(sequenceBlocks[i].orderNumber == (this.orderNumber - 1)) {
        if(sequenceBlocks[i].Color != GameplayManager.startColor) {
          return true;
        }
      }
    }
    return false;
  }
}

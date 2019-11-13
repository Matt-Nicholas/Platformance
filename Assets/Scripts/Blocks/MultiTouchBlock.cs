using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiTouchBlock:Block {

  List<Block> childBlocks = new List<Block>();
  public override void Start() {
    base.Start();
    childBlocks = gameObject.GetComponentsInChildren<Block>().ToList();
  }

  private void Entered(Color playerColor) {
    for(int i = 0; i < childBlocks.Count; i++) {
      if(childBlocks[i].Color != startColor && childBlocks[i].Color != playerColor) {
        childBlocks[i].SetColor(startColor);
        childBlocks[i].SetColor(playerColor);
        return;
      }
      else if(childBlocks[i].Color == startColor) {
        childBlocks[i].SetColor(playerColor);
        return;
      }
    }
  }
}

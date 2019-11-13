using UnityEngine;

public class UncolorBlock:Block {

  public override void Start() {
    base.Start();
  }

  private void Entered(Color playersColor) {
    if(currentColor != playersColor) {
      SetColor(playersColor);
    }
    else if(currentColor == playersColor) {
      SetColor(GameplayManager.startColor);
    }
  }
}

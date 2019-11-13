using System.Collections;
using UnityEngine;

public class Block:MonoBehaviour {

  public Color Color { get { return currentColor; } set { this.currentColor = value; } }

  protected Renderer rend;
  protected Color currentColor;
  protected Color startColor;

  private bool canBeChanged = true;

  public virtual void Start() {
    rend = GetComponent<Renderer>();

    SetColor(GameplayManager.startColor);

    //SetColor(Color.black);
    //StartCoroutine(ActivateBlock());
  }

  public virtual void SetColor(Color col) {
    //if(!TheGameManager.Instance.blocksAreActive) return;
    this.currentColor = col;
    rend.material.color = col;
  }


  IEnumerator ActivateBlock() {
    float delayTime = Random.Range(0.25f, 3.0f);

    yield return new WaitForSeconds(delayTime);
    SetColor(GameplayManager.startColor);

  }

}

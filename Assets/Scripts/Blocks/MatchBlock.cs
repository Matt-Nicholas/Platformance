using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBlock:Block {

  public Color colorToMatch;
  private Color m_StartColor;

  public bool matched = false;


  // Use this for initialization
  public override void Start() {
    base.Start();
    m_StartColor = new Color(colorToMatch.r, colorToMatch.g, colorToMatch.b, 0.5f);
    SetColor(m_StartColor);
    //sequenceBlocks = FindObjectsOfType(typeof(SequenceBlock)) as SequenceBlock[];

    //orderNumber = Convert.ToInt32(text.Trim());
  }

  private void Entered(Color color) {
    if(color == colorToMatch) {
      matched = true;
      SetColor(colorToMatch);
    }
    print("Block is matched: " + matched);
  }

  private void Exited(Color color) {

    if(color == colorToMatch) {
      matched = false;
      SetColor(m_StartColor);
    }
    print("Block is matched: " + matched);
  }

  // Update is called once per frame
  void Update() {

  }
}

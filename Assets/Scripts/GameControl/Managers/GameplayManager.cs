using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Timer))]
public class GameplayManager:MonoBehaviour {

  public static Color startColor = Color.white;

  private Block[] blocks;
  private Dictionary<Color/*player color*/, int/*number of blocks player owns*/> score = new Dictionary<Color, int>();
  private bool winnerDeclared = false;

  private Timer lapTimer;

  void Start() {

    blocks = GetAllBlocks();

    lapTimer = GetComponent<Timer>();
  }

  private Block[] GetAllBlocks() {
    return FindObjectsOfType(typeof(Block)) as Block[];
  }

  // Update is called once per frame
  void Update() {
    if(blocks.Length == 0) return;

    if(GameObject.Find("TimerDisplay") != null)
      lapTimer.UpdateTimerDisplay();

    // Set all blocks colored to true...
    bool levelComplete = AllBlocksColored();


    if(levelComplete && !winnerDeclared) {

      winnerDeclared = true;

      lapTimer.StopTimer();

      // count blocks of each color
      for(int i = 0; i < blocks.Length; i++) {
        if(!score.ContainsKey(blocks[i].Color)) {
          score[blocks[i].Color] = 0;
        }
        score[blocks[i].Color] += 1;
      }

      Color winner = startColor;

      int highestCount = 0;

      foreach(Color cColor in score.Keys) {
        if(score[cColor] > highestCount) {
          highestCount = score[cColor];
          winner = cColor;
        }
      }

      Debug.Log(winner.ToString() + "has won the game with a score of " + score[winner] + " blocks colored!");

    }


  }
  private bool AllBlocksColored() {

    if(blocks.Length == 0) {
      return false;
    }

    for(int i = 0; i < blocks.Length; i++) {
      if(blocks[i].Color == startColor || blocks[i].Color == Color.black) {
        return false;
      }
    }

    return true;
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

  Color color;

	void Start () {
    PlayerInput player = GetComponent<PlayerInput>();

    color = TheGameManager.Instance.players[player.playerNumber].Color;

  }

  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag.Equals("Block")) {
      other.gameObject.SendMessage("Entered", color, SendMessageOptions.DontRequireReceiver);
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if(other.tag.Equals("Block")) {
      other.gameObject.SendMessage("Exited", color, SendMessageOptions.DontRequireReceiver);
    }
  }

}

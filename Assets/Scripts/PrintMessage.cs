using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMessage : MonoBehaviour {
  public string message = "";
	// Use this for initialization
	void Start () {
	}
  private void OnEnable() {
    Debug.Log(message);
    
  }
  // Update is called once per frame
  void Update () {
		
	}
}

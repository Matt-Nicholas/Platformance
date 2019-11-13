using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void Move(Vector3 direction) {
    Vector3 newPosition = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z);
    transform.position = newPosition;
  }
}

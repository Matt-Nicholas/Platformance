using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

  Renderer rend;
	// Use this for initialization
	void Start () {
    rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		rend.material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x));
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer:MonoBehaviour {

  private Text timerText;

  public float CurrentLapTime {
    get { return currentLapTime; }
  }

  private bool clockIsTicking;
  private float currentLapTime = 0.0f;

  public Timer() { }

  void Update() {
    if(clockIsTicking) {
      currentLapTime += Time.deltaTime;
    }
  }

  public void StartTimer(float startTime = 0) {
    clockIsTicking = true;
    currentLapTime = startTime;
  }

  public float StopTimer() {
    clockIsTicking = false;
    return currentLapTime;
  }

  public void PauseTimer(bool paused) {
    clockIsTicking = paused;
  }

  public void UpdateTimerDisplay() {
    if(timerText == null) {
      timerText = GameObject.Find("TimerDisplay").GetComponent<Text>();
    }
    timerText.text = Math.Round(currentLapTime, 3).ToString();
  }
}

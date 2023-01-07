using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private bool clockIsTicking;
    private float currentLapTime = 0.0f;

    void Update()
    {
        if (clockIsTicking)
        {
            currentLapTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer(float startTime = 0)
    {
        clockIsTicking = true;
        currentLapTime = startTime;
    }

    public float StopTimer()
    {
        clockIsTicking = false;
        return currentLapTime;
    }

    public void PauseTimer(bool paused)
    {
        clockIsTicking = paused;
    }

    public void UpdateTimerDisplay()
    {
        timerText.text = Math.Round(currentLapTime, 3).ToString();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;
    [SerializeField] private float timeToDisplay = 0.0f;
    private bool _isRunning;


    public void ResetTimer()
    {
        timeToDisplay = 0.0f;
        _isRunning = true;
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning) return;

        if (timerType == TimerType.Countdown && timeToDisplay <= 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        float deltaTime = timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;
        timeToDisplay += deltaTime;

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.Max(timeToDisplay, 0.0f)); // Ensure timeToDisplay is non-negative
        _timerText.text = timeSpan.ToString(@"mm\:ss");
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    private DesktopApp desktopApp;

    private int days;
    private float seconds;
    private int minutes;
    private int hours;

    private bool timerStopped;

    private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        desktopApp = GetComponent<DesktopApp>();
        InitTimeManager();
    }

    private void Update()
    {
        if (!timerStopped)
        {
            UpdateTime();
            UpdateText();
        }
        else
        {
            if(!desktopApp.GetIsCreated())
            {
                StartDay();
            }
        }
    }

    private void InitTimeManager()
    {
        days = 0;

        seconds = 0.0f;
        minutes = 0;
        hours = 0;

        timerStopped = true;

        timerText = GetComponentInChildren<TextMeshProUGUI>();

        StartDay();
    }

    public void StartDay()
    {
        timerStopped = false;
        hours = 10;
        minutes = 0;
        seconds = 0.0f;

        days++;
    }

    public void EndDay()
    {
        timerStopped = true;
        hours = 2;
        minutes = 0;
        seconds = 0.0f;

        desktopApp.OpenApp();
    }

    private void UpdateTime()
    {
        seconds += Time.deltaTime * 84;

        if (seconds >= 60.0f)
        {
            seconds = 0;
            minutes++;
            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
                if (hours >= 24)
                {
                    hours = 0;
                }
                else if (hours == 2)
                {
                    EndDay();
                }
            }
        }
    }
    private void UpdateText()
    {
        timerText.text = hours.ToString("00") + ":" + minutes.ToString("00");
    }

    public int GetDays()
    {
        return days;
    }
}

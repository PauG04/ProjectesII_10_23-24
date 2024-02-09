using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    private int day;
    private int hour;
    private int minute;
    private float second;

    private bool isStopped;

    [SerializeField] int timeMultiplier;

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

        day = 1;
        hour = 20;
        minute = 0;
        second = 0;
    }

    private void Update()
    {
        if (!isStopped)
            UpdateTime();
    }

    private void UpdateTime()
    {
        second += Time.deltaTime * timeMultiplier;
        if (second >= 60)
        {
            second = 0;
            minute++;
            if (minute >= 60)
            {
                minute = 0;
                hour++;
                if (hour >= 24)
                {
                    hour = 0;
                    day++;
                }
                else if (hour == 5)
                {
                    StopTime();
                }
            }
        }
    }

    private void StopTime()
    {
        isStopped = true;
    }

    private void ResumeTime()
    {
        isStopped = false;
    }

    public int GetDay()
    {
        return day;
    }

    public int GetHour()
    {
        return hour;
    }

    public int GetMinute()
    {
        return minute;
    }
}

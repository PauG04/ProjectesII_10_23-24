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

    [SerializeField] private int realMinutesPerDay;
    [SerializeField] private int firstHour;
    [SerializeField] private int lastHour;
    [SerializeField] private float rent;
    private int hoursPerDay;
    private int timeMultiplier;

    [SerializeField] private GameObject endOfDayObject;

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
        hour = firstHour;
        minute = 0;
        second = 0;

        hoursPerDay = 24 - firstHour + lastHour;
        timeMultiplier = InitTimeMultiplier();
    }

    private void Update()
    {
        //if (!isStopped)
            //UpdateTime();
    }

    private int InitTimeMultiplier()
    {
        int realSecondsPerDay = realMinutesPerDay * 60;
        return hoursPerDay * 3600 / realSecondsPerDay;
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
                Debug.Log(hour);
                if (hour >= 24)
                {
                    hour = 0;
                    day++;
                }
                else if (hour == lastHour)
                {
                    StopTime();
                }
            }
        }
    }

    public void StopTime()
    {
        endOfDayObject.SetActive(true);
        //EconomyManager.instance.AddMoney(-rent);
        
    }

    public void ResumeTime()
    {
        endOfDayObject.SetActive(false);
        hour = firstHour;
        minute = 0;
        second = 0.0f;

        EconomyManager.instance.ResetDailyEarnings();
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

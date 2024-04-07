using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Header("Days")]
    [SerializeField] private int currentDay;
    [SerializeField] private int lastDay;

    [Header("Clients")]
    [SerializeField] private List<ClientNode> day1Clients;
    [SerializeField] private List<ClientNode> day2Clients;
    [SerializeField] private List<ClientNode> day3Clients;
    [SerializeField] private List<ClientNode> day4Clients;
    [SerializeField] private List<ClientNode> day5Clients;

    public List<ClientNode> GetClients(int day)
    {
        switch (day)
        {
            case 1:
                return day1Clients;
            case 2:
                return day2Clients;
            case 3:
                return day3Clients;
            case 4:
                return day4Clients;
            case 5:
                return day5Clients;
            default:
                return null;
        } 
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public int GetLastDay()
    {
        return lastDay;
    }
    public void NextDay(int i)
    {
        currentDay += i;
    }
    public void SetCurrentDay(int currentDay)
    {
        this.currentDay = currentDay;
        GetClients(currentDay);
    }
    public void SavePref()
    {
        PlayerPrefs.SetInt("CurrentDay", currentDay);
    }
    public void LoadPref()
    {
        currentDay = PlayerPrefs.GetInt("CurrentDay", 1);
    }
}

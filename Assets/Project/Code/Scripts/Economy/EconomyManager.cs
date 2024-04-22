using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance { get; private set; }

    private float dailyEarnings;
    private float dailyExpanses;
    private float money = 2030.0f;
    private float moneyChanged;

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
        dailyEarnings = 0.0f;
        dailyExpanses = 0.0f;
    }

    public void AddMoney(float earnings)
    {
        if(earnings > 0.0f)
        {
            dailyEarnings += earnings;         
        }
        money += earnings;
    }

    public void ResetDailyEarnings()
    {
        dailyEarnings = 0.0f;
        dailyExpanses = 0.0f;
    }
    public float GetMoney()
    {
        return money;
    }
    public float GetDailyEarnings()
    {
        return dailyEarnings;
    }
    public float GetDailyExpanses()
    {
        return dailyExpanses;
    }
    public float GetMoneyChanged()
    {
        return moneyChanged;
    }

    public void SetMoney(float _money)
    {
        money = _money;
    }

    public void SetMoneyChanged(float earnings)
    {
        if (earnings > 0)
        {
            dailyEarnings += earnings;
        }
        else
        {
            dailyExpanses += earnings;
        }
        moneyChanged = earnings;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetFloat("money", money);
    }
}

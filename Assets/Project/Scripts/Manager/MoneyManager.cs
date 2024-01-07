using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance { get; private set; }

    private float playerMoney;
    private float dayEarnings;
    private float dayDebts;

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

        playerMoney = 0.0f;
        dayEarnings = 0.0f;
	    dayDebts = 30.0f;
    }

    public float CalculateEndOfDayMoney()
	{
		playerMoney += dayEarnings - dayDebts;
	    return playerMoney;
    }

    public float GetPlayerMoney()
    {
        return playerMoney;
    }
    public float GetDayEarnings()
    {
        return dayEarnings;
    }
    public float GetDayDebts()
    {
        return dayDebts;
    }

    public void AddPlayerMoney(float money)
    {
        playerMoney = money;
    }
    public void AddDayEarnings(float money)
    {
        dayEarnings = money;
    }
    public void SetDayDebts(float money)
    {
        dayDebts = money;
    }

}

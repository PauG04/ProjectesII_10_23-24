using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI dailyEarningsText;
    [SerializeField] private TextMeshProUGUI dailyExpensesText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    private float dailyEarnings;
    private float dailyExpenses;
    private float money;
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
        dailyExpenses = 0.0f;
        money = 2000.0f;
    }

    public void AddMoney(float earnings)
    {
        if(earnings > 0.0f)
        {
            dailyEarnings += earnings;         
        }
        else
        {
            dailyExpenses += earnings;
        }
        money += earnings;
    }



    public void SetMoneyText()
    {
        dailyEarningsText.text = "Money Earned: " + dailyEarnings.ToString();
        dailyExpensesText.text = "Expenses: " + dailyExpenses.ToString();
        totalMoneyText.text = "Total Money: " + money.ToString();
    }

    public void ResetDailyEarnings()
    {
        dailyEarnings = 0.0f;
    }
    public void ResetDailyExpense()
    {
        dailyExpenses = 0.0f;
    }
    public float GetMoney()
    {
        return money;
    }

    public float GetMoneyChaned()
    {
        return moneyChanged;
    }

    public void SetMoney(float _money)
    {
        money = _money;
    }

    public void SetMoneyChanged(float earnings)
    {
        moneyChanged = earnings;
    }

}

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
        dailyEarnings = 0.0f;
        money = 0.0f;
    }

    public void AddMoney(float earnings)
    {
        dailyEarnings += earnings;
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
}

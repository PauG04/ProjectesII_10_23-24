using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndOfDayText : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI moneyText;

    private void Awake()
    {
        titleText = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();

        SetText();
    }

    public void SetText()
    {
        titleText.text = "Day " + TimeManager.instance.GetDays() + " Finished!!";

        moneyText.text =
            "Your Money: " + MoneyManager.instance.GetPlayerMoney() +
            "\nToday's Earnings: " + MoneyManager.instance.GetDayEarnings() +
            "\nToday's Debt: " + MoneyManager.instance.GetDayDebts() +
            "\nTotal Money: " + MoneyManager.instance.CalculateEndOfDayMoney();

    }


}

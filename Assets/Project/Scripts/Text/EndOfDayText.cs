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
        Debug.Log(titleText.text);

        SetText();
    }

    public void SetText()
    {
        titleText.text = "Day " + 10 + " Finished!!";
        Debug.Log(titleText.text);

        //Falta hacer un MoneyManager
        moneyText.text =
            "Your Money: " + 1000 +
            "\nToday's Earnings: " + 1000 +
            "\nToday's Debt: " + 1000 +
            "\nTotal Money: " + 1000;

    }


}

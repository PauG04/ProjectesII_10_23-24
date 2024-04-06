using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndOfDay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dailyEarnings;
    [SerializeField] private TextMeshProUGUI dailyExpenses;

    [SerializeField] private TextMeshProUGUI totalMoney;

    private void Update()
    {
        dailyEarnings.text = "Ganancias: " + EconomyManager.instance.GetDailyEarnings().ToString("00.00") + "€";
        dailyExpenses.text = "Gastos: " + EconomyManager.instance.GetDailyExpanses().ToString("00.00") + "€";

        totalMoney.text = "Dinero: " + EconomyManager.instance.GetMoney().ToString("00.00") + "€";
    }
}

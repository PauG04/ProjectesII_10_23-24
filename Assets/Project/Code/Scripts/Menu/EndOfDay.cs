using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dailyEarnings;
    [SerializeField] private TextMeshProUGUI dailyExpenses;

    [SerializeField] private TextMeshProUGUI totalMoney;
    [SerializeField] private DayManager dayManager;

    private string lastText;

    private void Update()
    {
        if(dayManager.GetCurrentDay() < dayManager.GetLastDay())
        {
            dailyEarnings.text = "Ganancias: " + EconomyManager.instance.GetDailyEarnings().ToString("00.00") + "€";
            dailyExpenses.text = "Gastos: " + EconomyManager.instance.GetDailyExpanses().ToString("00.00") + "€";

            totalMoney.text = "Dinero: " + EconomyManager.instance.GetMoney().ToString("00.00") + "€";
        }
        else
        {
            dailyExpenses.text = lastText;
            dailyExpenses.fontSize = 75;
            dailyEarnings.text = " ";
            totalMoney.text = " ";
        }

    }

    public void LoadDay()
    {
        if (ClientManager.instance.GetCourtainClosed() && dayManager.GetCurrentDay() < dayManager.GetLastDay())
        {
            ClientManager.instance.LoadDay();
        }
        else if(dayManager.GetCurrentDay() == dayManager.GetLastDay())
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SetMesage(bool state)
    {
        if(state)
        {
            lastText = "Enhorabuena, has pagado el alquiler de esta semana, solo te queda seguir pagando hasta que te jubiles";
        }
        else
        {
            lastText = "Vaya vago, te has gastado todo el dinero en gilipolleces y te han hechado, bueno, por lo menos ya no trabajaras en un bar de moros";
        }
    }
}

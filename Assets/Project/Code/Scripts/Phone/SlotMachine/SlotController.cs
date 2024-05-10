using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class SlotController : MonoBehaviour
{
    public static event Action SpinSlot = delegate { };

    [Header("Rows")]
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private TextMeshProUGUI currentBetText;
    [SerializeField] private int[] typeOfBets;

    [Header("Rows")]
    [SerializeField] private SlotRow[] rows;

    private int currentBetIndex;
    private int playerMoney = 1;

    private float prizeValue;
    private bool resultsChecked = false;

    private void Start()
    {
        currentBetIndex = 0;
    }

    public void AddCurrentBet()
    {
        currentBetIndex++;

        if (currentBetIndex >= typeOfBets.Length)
        {
            currentBetIndex = 0;
        }

        playerMoney = typeOfBets[currentBetIndex];

    }
    public void DecraseCurrentBet()
    {
        currentBetIndex--;

        if (currentBetIndex < 0)
        {
            currentBetIndex = typeOfBets.Length - 1;
        }

        playerMoney = typeOfBets[currentBetIndex];
    }

    private void Update()
    {
        currentMoney.text = EconomyManager.instance.GetMoney().ToString("00.00");
        currentBetText.text = playerMoney.ToString("00");

        if (!rows.All(row => row.GetRowStopped()))
        {
            prizeValue = 0;
            resultsChecked = false;
        }

        if (rows.All(row => row.GetRowStopped()) && !resultsChecked)
        {
            CheckResults();
        }
    }

    public void SpinSlotMachine()
    {
        if (typeOfBets[currentBetIndex] <= EconomyManager.instance.GetMoney())
        {
            if (rows.All(row => row.GetRowStopped()))
            {
                EconomyManager.instance.SetMoneyChanged(-playerMoney);
                SpinSlot();
            }
        }
    }
    private void CheckResults()
    {
        if (rows.All(row => row.GetStoppedSlot() == "Bar"))
        {
            prizeValue = playerMoney * 10;
        }
        else if (rows.All(row => row.GetStoppedSlot() == "Beer"))
        {
            prizeValue = playerMoney * 7;
        }
        else if (
            rows.All(row => row.GetStoppedSlot() == "DrinkGreen")
            || rows.All(row => row.GetStoppedSlot() == "DrinkOrange")
            || rows.All(row => row.GetStoppedSlot() == "DrinkPink")
            || rows.All(row => row.GetStoppedSlot() == "DrinkRed")
            || rows.All(row => row.GetStoppedSlot() == "DrinkBrown")
            )
        {
            prizeValue = playerMoney * 5;
        }

        resultsChecked = true;

        EconomyManager.instance.SetMoneyChanged(prizeValue);

        prizeValue = 0;
    }

    public bool GetResultChecked()
    {
        return resultsChecked;
    }
}

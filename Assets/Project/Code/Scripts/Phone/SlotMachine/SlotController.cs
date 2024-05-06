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
    private bool resultsCheked = false;

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
        currentMoney.text = EconomyManager.instance.GetMoney() + "";
        currentBetText.text = playerMoney.ToString("00");

        if (!rows.All(row => row.GetRowStopped()))
        {
            prizeValue = 0;
            resultsCheked = false;
        }

        if (rows.All(row => row.GetRowStopped()) && !resultsCheked)
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
                EconomyManager.instance.AddMoney(-playerMoney);
                SpinSlot();
            }
        }
    }
    private void CheckResults()
    {
        if (rows.All(row => row.GetStoppedSlot() == "Bar"))
        {
            prizeValue = playerMoney * 7;
        }
        else if (rows.All(row => row.GetStoppedSlot() == "Beer"))
        {
            prizeValue = playerMoney * 4;
        }
        else if (rows.All(row => row.GetStoppedSlot() == "Drinks"))
        {
            prizeValue = playerMoney * 2;
        }

        resultsCheked = true;

        EconomyManager.instance.AddMoney(prizeValue);
        prizeValue = 0;
    }
}

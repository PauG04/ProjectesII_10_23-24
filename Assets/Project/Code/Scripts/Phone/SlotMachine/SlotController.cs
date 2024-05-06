using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class SlotController : MonoBehaviour
{
    public static event Action SpinSlot = delegate { };

    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private TextMeshProUGUI currentBet;
    [SerializeField] private SlotRow[] rows;

    private float playerMoney = 1;

    private float prizeValue;
    private bool resultsCheked = false;

    private void Start()
    {
        currentBet.text = "01";
    }

    private void Update()
    {
        currentMoney.text = EconomyManager.instance.GetMoney() + "";

        if (!rows.All(row => row.GetRowStopped()))
        {
            prizeValue = 0;
            resultsCheked = false;
        }

        if (rows.Any(row => row.GetRowStopped()) && !resultsCheked)
        {
            CheckResults();
        }
    }

    public void SpinSlotMachine()
    {
        if (rows.All(row => row.GetRowStopped()))
        {
            EconomyManager.instance.AddMoney(-playerMoney);
            SpinSlot();
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
            prizeValue = playerMoney * 3;
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

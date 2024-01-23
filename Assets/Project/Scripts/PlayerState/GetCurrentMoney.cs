using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetCurrentMoney : MonoBehaviour
{
    private TextMeshProUGUI currentMoney;

    private void Awake()
    {
        currentMoney = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        currentMoney.text = MoneyManager.instance.GetPlayerMoney() + "$";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopApp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoney;

    private void Update()
    {
        if (textMoney != null)
        {
            textMoney.text = EconomyManager.instance.GetMoney().ToString("00.00") + "€";
        }
    }
}

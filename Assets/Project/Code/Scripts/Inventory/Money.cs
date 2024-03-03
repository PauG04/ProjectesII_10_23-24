using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Money : MonoBehaviour
{
    [Header("Lerp")]
    [SerializeField] private float Velocity;

    private TextMeshPro textMesh;

    private float money;
    private bool growing;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        growing = true;
    }

    private void Update()
    {       
        money = EconomyManager.instance.GetMoneyChaned();
        if(money != 0)
        {
            MoneyLerp();
        }
        else
        {
            textMesh.text = EconomyManager.instance.GetMoney().ToString("00.00") + '€';
        }
    }

    private void MoneyLerp()
    {
        if(money > 0)
        {
            textMesh.color = Color.green;
        }
        else
        {
            textMesh.color = Color.red;
        }

        textMesh.text = money.ToString("00.00") + '€';

        if(growing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2.15f, 2.15f, 2.15f), Time.deltaTime * Velocity);
            if (transform.localScale.x >= 2.10)
            {
                growing = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * Velocity);
            if (transform.localScale.x <= 1.05)
            {
                growing = true;
                EconomyManager.instance.SetMoney(EconomyManager.instance.GetMoney() + money);
                transform.localScale = Vector3.one;
                textMesh.color = Color.white;
                EconomyManager.instance.SetMoneyChanged(0);
            }
        }
        
    }
}

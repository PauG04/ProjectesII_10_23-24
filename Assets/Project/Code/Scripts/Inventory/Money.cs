using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject moneyInformation;

    private TextMeshPro textMesh;

    private float money;
    

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {       
        money = EconomyManager.instance.GetMoneyChanged();
        if(money != 0)
        {
            EconomyManager.instance.SetMoney(EconomyManager.instance.GetMoney() + money);
            MoneyLerp();
            EconomyManager.instance.SetMoneyChanged(0);
        }
        else
        {
            textMesh.text = EconomyManager.instance.GetMoney().ToString("00.00") + '€';
        }
    }

    private void MoneyLerp()
    {
        GameObject information = Instantiate(moneyInformation);
        information.transform.SetParent(gameObject.transform, true);
        information.transform.localPosition = new Vector3(0, 0, -1);

        information.GetComponent<TextMeshPro>().text = money.ToString("00.00") + '€';
        if (money > 0)
        {
            AudioManager.instance.PlaySFX("EarnMoney");
            information.GetComponent<TextMeshPro>().color = Color.green;

        }
        else if(money < 0) 
        {
            AudioManager.instance.PlaySFX("LoseMoney");
            information.GetComponent<TextMeshPro>().color = Color.red;
        }
    }
}

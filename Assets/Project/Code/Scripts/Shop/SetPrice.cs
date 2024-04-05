using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPrice : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private BuyLiquid money;

    [SerializeField] private GameObject father;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        money = transform.parent.transform.parent.GetComponent<BuyLiquid>();
    }

    private void Update()
    {
        if(father.transform.childCount <= 1)
        {
            textMesh.text = money.GetPrice().ToString("00.00") + '€';
            textMesh.color = Color.black;
        }
        else
        {
            textMesh.text = "LLENO";
            textMesh.color = Color.red;
        }
        
    }
}

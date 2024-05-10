using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPrice : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private BuyShop money;

    [SerializeField] private GameObject parent;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        money = transform.parent.transform.parent.GetComponent<BuyShop>();
    }

    private void Update()
    {
        if(parent.transform.childCount <= 1)
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

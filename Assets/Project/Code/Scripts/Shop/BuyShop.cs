using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyLiquid : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private GameObject item;
    [SerializeField] private float price;

    [Header("Father")]
    [SerializeField] private GameObject father;

    private Vector2 position;


    private void Start()
    {
        position = new Vector2(-0.12f, 0.07f);
    }


    public void Buy()
    {
        if (father.transform.childCount <= 1)
        {
            EconomyManager.instance.SetMoneyChanged(-price);
            GameObject newItem = Instantiate(item);

            if(father.transform.childCount == 1)
            {
                newItem.transform.SetParent(father.transform, true);
                Destroy(newItem.GetComponent<PolygonCollider2D>());
                newItem.GetComponent<SpriteRenderer>().color = Color.grey;
                newItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newItem.GetComponent<DragItems>().enabled = false;
                newItem.GetComponent<ArrowManager>().enabled = false;
                newItem.transform.GetChild(3).gameObject.SetActive(false);

                newItem.transform.localPosition = position;
            }
            else
            {
                newItem.transform.SetParent(father.transform, true);
                newItem.transform.localPosition = Vector2.zero;
                newItem.GetComponent<DragItems>().SetInitPosition(Vector2.zero);
            }
        }
    }

    public float GetPrice()
    {
        return price;
    }
}

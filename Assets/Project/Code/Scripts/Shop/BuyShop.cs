using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyShop : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private GameObject item;
    [SerializeField] private float price;

    [Header("Parent")]
    [SerializeField] private GameObject parent;

    private Vector2 position = new Vector2(-0.12f, 0.07f);

    public void Buy()
    {
        if (parent.transform.childCount <= 1)
        {
            if (EconomyManager.instance.GetMoney() < price) 
                return;

            EconomyManager.instance.SetMoneyChanged(-price);
            GameObject newItem = Instantiate(item);

            if(parent.transform.childCount == 1)
            {
                CreateBackgroundDrink(newItem);
            }
            else
            {
                CreateDrink(newItem);
            }
        }
    }
    public void CreateBackgroundDrink(GameObject newItem)
    {
        newItem.transform.SetParent(parent.transform, true);
        Destroy(newItem.GetComponent<PolygonCollider2D>());
        newItem.GetComponent<SpriteRenderer>().color = Color.grey;
        newItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
        newItem.GetComponent<DragItems>().enabled = false;
        newItem.GetComponent<ArrowManager>().enabled = false;
        newItem.transform.GetChild(3).gameObject.SetActive(false);

        newItem.transform.localPosition = position;
    }
    public void CreateDrink(GameObject newItem)
    {
        newItem.transform.SetParent(parent.transform, true);
        newItem.transform.localPosition = Vector2.zero;
        newItem.GetComponent<DragItems>().SetInitPosition(Vector2.zero);
    }
    public float GetPrice()
    {
        return price;
    }
    public GameObject GetObject()
    {
        return item;
    }
    public Vector2 GetPosition()
    {
        return position;
    }
}

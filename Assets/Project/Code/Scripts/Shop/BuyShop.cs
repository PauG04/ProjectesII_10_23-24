using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyLiquid : MonoBehaviour
{
    [Header("item")]
    [SerializeField] private GameObject item;
    [SerializeField] private float price;
    [SerializeField] private GameObject recreateObject;
    [SerializeField] private GameObject _parent;
    [SerializeField] private ItemNode _node;

    [Header("Boolean")]
    [SerializeField] private bool isItem;

    private SpriteRenderer childSprite;


    private Vector3 _position;

    private void Start()
    {
        _position = item.transform.localPosition;
        childSprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        childSprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }


    private void OnMouseDown()
    {
        if(EconomyManager.instance.GetMoney() > price)
        {
            if (item != null)
            {
                if (isItem)
                {
                    InventoryManager.instance.AddItem(_node);
                }
                else
                {
                    item.GetComponentInChildren<LiquidManager>().SetCurrentLiquid();
                }
            }
            else
            {
                GameObject _item = Instantiate(recreateObject, _position, Quaternion.identity);
                item = _item;
                _item.transform.SetParent(_parent.transform, true);
            }
            EconomyManager.instance.AddMoney(-price);
        }      
    }

    public float GetPrice()
    {
        return price;
    }
}

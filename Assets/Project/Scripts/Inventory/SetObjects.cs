using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SetObjects : MonoBehaviour
{
    [SerializeField] private RectTransform itemContent;
    [SerializeField] private GameObject inventoryItem;

    private void Awake()
    {
        //ListItems();
    }

    public void ListItems()
    {
        foreach (RectTransform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (KeyValuePair<Item, int> item in InventoryManager.instance.GetItems())
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.icon;
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Key.itemName + "\nx" + item.Value;
        }
    }

    private void Update()
    {
        ListItems();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnjectInBucket : MonoBehaviour
{
    [SerializeField] private GameObject objectInBucket;
    [SerializeField] private Item ice;

    private void OnMouseDown()
    {
        if(InventoryManager.instance.DoesKeyExist(ice))
        {
            GameObject newGlass1 = Instantiate(objectInBucket, transform);
            newGlass1.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            newGlass1.tag = "Decoration";
            newGlass1.transform.SetParent(null);
            InventoryManager.instance.UseItem(ice);
        }      
    }
}

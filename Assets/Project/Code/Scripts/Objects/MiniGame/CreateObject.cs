using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject createdObject;

    [Header("Bucket")]
    [SerializeField] private GameObject bucket;

    private bool isCreated;
    private void OnMouseDown()
    {
        if(InventoryManager.instance.UseItem(createdObject.GetComponent<GetItemInformation>().GetItemGroupNode()))
        {
            GameObject item = Instantiate(createdObject, transform);
            item.transform.SetParent(null);
            item.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            item.GetComponent<DragItemsNew>().SetIsDragging(true);
            item.GetComponent<DragItemsNew>().SetInitPosition(transform.position);

            if (!isCreated)
            {
                isCreated = true;
            }

            if (bucket != null)
            {
                item.GetComponent<GetItemInformation>().SetBucket(bucket);
            }
        }
    }

    public bool GetIsCreated()
    {
        return isCreated;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemGroup : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject createdObject;

    [Header("Bucket")]
    [SerializeField] private GameObject bucket;

    [Header("Tutorial")]
    [SerializeField] private TutorialManager tutorial;

    private bool isCreated;
    private void OnMouseDown()
    {
        if (InventoryManager.instance.UseItem(createdObject.GetComponent<GetItemInformation>().GetItemGroupNode()))
        {
            GameObject item = Instantiate(createdObject, transform);
            item.transform.SetParent(null);
            item.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            item.GetComponent<DragItems>().SetIsDragging(true);
            item.GetComponent<DragItems>().SetInitPosition(transform.position);

            if (!isCreated)
            {
                isCreated = true;
            }

            if (bucket != null)
            {
                item.GetComponent<GetItemInformation>().SetBucket(bucket);
            }

            if (tutorial != null)
            {
                tutorial.SetIce(item);
            }
        }
    }

    public bool GetIsCreated()
    {
        return isCreated;
    }
}

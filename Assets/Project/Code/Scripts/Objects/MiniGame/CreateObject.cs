using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject createdObject;

    [Header("Bucket")]
    [SerializeField] private GameObject bucket;

    [Header("Tutorial")]
    [SerializeField] private TutorialManager tutorial;

    private SpriteRenderer sprite;

    private bool isCreated;
    private ItemNode nodeItem;

    private void Awake()
    {
        nodeItem = createdObject.GetComponent<SetItemInGlass>().GetItemNode();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (InventoryManager.instance.GetItems()[nodeItem] > 0)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }
    }
    private void OnMouseDown()
    {
        if (InventoryManager.instance.UseItem(createdObject.GetComponent<SetItemInGlass>().GetItemNode()))
        {
            GameObject item = Instantiate(createdObject, transform);
            item.transform.SetParent(null);
            item.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            item.GetComponent<DragItems>().SetIsDragging(true);
            item.GetComponent<DragItems>().SetInitPosition(transform.position);
            item.GetComponent<Rigidbody2D>().mass = 0.01f;

            if (!isCreated)
            {
                isCreated = true;
            }

            if (bucket != null)
            {
                item.GetComponent<GetItemInformation>().SetBucket(bucket);
            }

            if(tutorial != null)
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

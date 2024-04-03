using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemInGlass : MonoBehaviour
{
    [SerializeField] private bool isInGlass;

    private DragItems drag;

    [SerializeField] private ItemNode itemNode;

    private void Start()
    {
        drag = GetComponent<DragItems>();
    }

    private void Update()
    {
        if (!isInGlass)
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Decoration") && drag.GetInsideWorkspace())
        {
            transform.SetParent(collision.transform);
            drag.enabled = false;
            isInGlass = true;
            collision.GetComponent<InsideDecorations>().AddItem(itemNode);
            drag.SetHasToReturn(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Decoration"))
        {
            drag.enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isInGlass = false;
            collision.GetComponent<InsideDecorations>().SubstractItem(itemNode);
            drag.SetHasToReturn(true);
        }
    }

    public ItemNode GetItemNode()
    {
        return itemNode;
    }
}

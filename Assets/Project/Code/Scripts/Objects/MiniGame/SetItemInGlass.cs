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

            Color itemColor = GetComponent<SpriteRenderer>().color;

            itemColor = new Color(itemColor.r, itemColor.g, itemColor.b, 0.5f);

            GetComponent<SpriteRenderer>().color = itemColor;
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

            Color itemColor = GetComponent<SpriteRenderer>().color;

            itemColor = new Color(itemColor.r, itemColor.g, itemColor.b, 1f);

            GetComponent<SpriteRenderer>().color = itemColor;

            drag.SetHasToReturn(true);
        }
    }

    public ItemNode GetItemNode()
    {
        return itemNode;
    }
}

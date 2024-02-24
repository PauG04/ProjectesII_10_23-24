using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemInGlass : MonoBehaviour
{
    [SerializeField] private bool isInGlass;

    private DragItems drag;

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
            GetComponent<DragItems>().enabled = false;
            isInGlass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Decoration"))
        {           
            GetComponent<DragItems>().enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isInGlass = false;
        }
    }
}

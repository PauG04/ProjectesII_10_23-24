using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCutPosition : MonoBehaviour
{
    private DragItemsNew dragItem;

    [Header("Childs and Cut")]
    [SerializeField] private GameObject cutPosition;
    [SerializeField] private List<GameObject> childLemon;

    [Header("Force")]
    [SerializeField] private float force;

    private GameObject bucket;
    private float width;

    private void Start()
    {
        dragItem= GetComponent<DragItemsNew>();

        width = childLemon[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;

        cutPosition.transform.position = new Vector2(childLemon[0].transform.position.x - width, childLemon[0].transform.position.y);
        cutPosition.SetActive(false);

        bucket = GetComponent<GetBucket>().GetBuckets();
        for (int i = 0; i < childLemon.Count(); i++)
        {
            childLemon[i].GetComponent<DragItemsNew>().SetInitPosition(bucket.transform.position);
        }
    }

    private void Update()
    {
        ActivceCut();
    }

    private void CutPosition()
    {
        if (childLemon.Count() > 1)
        {
            width = childLemon[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            cutPosition.transform.position = new Vector2(childLemon[0].transform.position.x - width, childLemon[0].transform.position.y);
        }
        else if(childLemon.Count() > 0)
        {
            FreeChild();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ActivceCut()
    {
        if (dragItem.GetInsideWorkspace())
        {
            cutPosition.SetActive(true);
        }
        else
        {
            cutPosition.SetActive(false);
        }
    }

    public void FreeChild()
    {
        childLemon[0].GetComponent<DragItemsNew>().enabled = true;
        childLemon[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        childLemon[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
        childLemon[0].transform.SetParent(null);       
        childLemon.Remove(childLemon[0]);
        CutPosition();
    }
}

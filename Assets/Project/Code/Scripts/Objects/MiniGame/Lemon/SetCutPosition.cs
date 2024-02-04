using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCutPosition : MonoBehaviour
{
    private DragItem dragItem;

    [Header("Childs and Cut")]
    [SerializeField] private GameObject cutPosition;
    [SerializeField] private List<GameObject> childLemon;

    [Header("Force")]
    [SerializeField] private float force;

    private void Start()
    {
        dragItem= GetComponent<DragItem>();

        cutPosition.transform.position = new Vector2(childLemon[0].transform.position.x - 0.1f, childLemon[0].transform.position.y);
        cutPosition.SetActive(false);
    }

    private void Update()
    {
        CutPosition();
        ActivceCut();
    }

    private void CutPosition()
    {
        if (childLemon.Count() > 0)
        {
            cutPosition.transform.position = new Vector2(childLemon[0].transform.position.x - 0.1f, childLemon[0].transform.position.y);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ActivceCut()
    {
        if (dragItem.GetIsInWorkSpace())
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
        childLemon[0].GetComponent<TakeLemonBucket>().SetStartPostion();
        childLemon[0].GetComponent<Rigidbody2D>().isKinematic= false;
        childLemon[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
        childLemon[0].transform.SetParent(null);       
        childLemon.Remove(childLemon[0]);
    }

    public void SetBucket(GameObject bucket)
    {
        for(int i = 0; i<childLemon.Count(); i++)
        {
            childLemon[i].GetComponent<TakeLemonBucket>().SetBucket(bucket);
        }
    }
}

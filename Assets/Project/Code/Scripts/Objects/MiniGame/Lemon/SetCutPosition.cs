using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCutPosition : MonoBehaviour
{
    [Header("Childs and Cut")]
    [SerializeField] private GameObject cutPosition;
    [SerializeField] private List<GameObject> childLemon;

    [Header("Force")]
    [SerializeField] private float force;

    private void Start()
    {
        cutPosition.transform.position = new Vector2(childLemon[0].transform.position.x - 0.1f, childLemon[0].transform.position.y);
    }

    private void Update()
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

    public void FreeChild()
    {
        childLemon[0].GetComponent<TakeLemonBucket>().SetStartPostion();
        childLemon[0].GetComponent<Rigidbody2D>().isKinematic= false;
        childLemon[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
        childLemon[0].transform.SetParent(null);       
        childLemon.Remove(childLemon[0]);
    }
}

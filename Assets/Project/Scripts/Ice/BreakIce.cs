using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIce : MonoBehaviour
{
    private FixedJoint2D fixedJoint2;

    private void Start()
    {
        fixedJoint2= GetComponent<FixedJoint2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hammer"))
        {
            Destroy(fixedJoint2);
        }
    }
}

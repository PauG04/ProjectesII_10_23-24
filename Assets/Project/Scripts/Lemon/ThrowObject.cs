using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    Rigidbody2D rb;
    private Transform positionParent;

    private void Awake()
    {
        positionParent = gameObject.transform.parent.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //if(transform.localPosition.y < 0)
        //{
        //    Destroy(gameObject);
        //}
    }
    public void Throw(float force)
    {
        Vector3 directionToCenter = ((Vector3.zero - transform.position) + positionParent.position);
        Vector3 direction = new Vector3(directionToCenter.x * 1f, 1, 0);

        rb.AddForce(direction * force, ForceMode2D.Impulse);

        float forceRot = force * 0.5f;

        rb.AddTorque(forceRot, ForceMode2D.Impulse);
    }
   
}

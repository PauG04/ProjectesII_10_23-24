using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDown;
    private bool canShake;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        canShake = false;
    }

    private void Update()
    {
        if (rb.velocity.y <= 0.00001f && rb.velocity.y >= -0.00001f)
            canShake = false;
        else
            canShake = true;
        if (isDown && rb.velocity.y >= 0 && canShake)
        {
            isDown = false;
            Debug.Log("vamos pa riba");
        }
        else if(!isDown && rb.velocity.y <= 0 && canShake) 
        { 
            isDown= true;
            Debug.Log("vamos pa abajo");
        }
    }
}

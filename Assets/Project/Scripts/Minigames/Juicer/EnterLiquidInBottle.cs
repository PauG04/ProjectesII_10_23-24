using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLiquidInBottle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fluid")
        {
            Debug.Log("Enter Liquid in the Bottle");
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBottle : MonoBehaviour
{
    [SerializeField]
    private GameObject shaker;
    [SerializeField]
    private Vector3 distance;
    private float rotation;


    private void Update()
    {
        //if (GameObject.Find("Shaker") != null && drag.GetDragging())
        //{
        //    shaker = GameObject.Find("Shaker");
        //    CalculateDistance();
        //    if (distance.y < 0.5 && distance.y > -0.5)
        //        Rotation();
        //    else
        //        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        //}
       
    }

    private void CalculateDistance()
    {
        //distance = transform.position - shaker.transform.position;
    }

    private void Rotation()
    {
        //if (distance.x < 0 && distance.x > -0.5)
        //{
        //    rotation = distance.x + 0.5f; 
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - rotation * 120);
        //}
        //else if(distance.x > 0 && distance.x < 0.5)
        //{
        //    rotation = distance.x - 0.5f;
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - rotation * 120);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        //}
            

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Windows;

public class DragHammer : MonoBehaviour
{
    private bool dragging;
    private float angle;
    private bool isATurn;
    private bool startFalling;
    private float force;
    private Vector2 position;
    private TargetJoint2D targetJoint;
    [SerializeField] private IceBreaking ice;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        angle = 0;
        force = 0;
        transform.localRotation= Quaternion.Euler(0,0,angle);
        position = transform.localPosition;
    }

    private void Update()
    {
        CalculatePosition();
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        if(startFalling && angle <= 0)
        {
            angle += 0.5f;
            if(angle >= 0) 
            {
                ice.BreakIce();
            }
        }
    }

    private void CalculatePosition()
    {
        //if (dragging)
        //    targetJoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //else
        //    targetJoint.transform.position = position;
        if(Input.GetKey(KeyCode.A) && isATurn && angle>-90) 
        {
            angle-=0.2f;
            isATurn = false;
        }
        else if (Input.GetKey(KeyCode.D) && !isATurn && angle>-90)
        {
            angle -= 0.2f;
            isATurn = true;
        }
        else if(angle <=- 90)
        {
            startFalling = true;
        }

    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}



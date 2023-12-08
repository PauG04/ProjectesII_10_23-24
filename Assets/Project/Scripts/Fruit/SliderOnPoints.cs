using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderOnPoints : MonoBehaviour
{
    [SerializeField] private CircleCollider2D[] points;

    private BoxCollider2D collider;
    private int id;
    private bool isPressing;
    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        id = 0;
    }

    private void Update()
    {
        if(isPressing)
        {
            DetectCollision();
        }    
    }

    private void DetectCollision()
    {
        if (collider.IsTouching(points[id]))
        {
            id++;
        }
        if (id == points.Length) 
        {
            id = 0;
        }
            
    }

    private void OnMouseDown()
    {
        isPressing = true;
    }

    private void OnMouseUp()
    {
        isPressing= false;
        id = 0;
    }
}

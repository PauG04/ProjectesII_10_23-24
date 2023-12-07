using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderOnPoints : MonoBehaviour
{

    [SerializeField] private BoxCollider2D[] points;

    private Slider collision;
    private int id;
    private bool isPressing;
    private void Start()
    {
        collision = GetComponent<Slider>();
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
        if (collision.GetCollider().IsTouching(points[id]))
        {
            id++;
            Debug.Log(id);
        }
        if(id == points.Length) 
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
    }
}

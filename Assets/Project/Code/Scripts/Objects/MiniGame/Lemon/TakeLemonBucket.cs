using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeLemonBucket : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 startPosition;

    private bool startLerp;
    private bool cut;

    private GameObject bucket;

    [Header("Magnitude")]
    [SerializeField] private float maxMagnitude;

    [Header("Velocity")]
    [SerializeField] private float velocity;

    [Header("Drag")]
    [SerializeField] private DragItem dragItem;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startLerp = false;
        cut = false;
    }

    private void Update()
    {
        stopObject();
        if (startLerp)
        {
            transform.position = Vector2.Lerp(transform.position, bucket.transform.position, Time.deltaTime * velocity);
            if (transform.position.y > bucket.transform.position.y - 0.02 && transform.position.y < bucket.transform.position.y + 0.02)
            {
                Destroy(gameObject);
            }
        }
    }

    private void stopObject()
    {
        if (transform.position.x > startPosition.x + maxMagnitude) 
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnMouseDown()
    {
        if(cut)
        {
            startLerp = true;
        }
        else
        {
            dragItem.SetIsDragging(true);
        }
            
    }

    public void SetStartPostion()
    {
        cut = true;
        startPosition = transform.position;
    }

    public void SetBucket(GameObject bucket)
    {
        this.bucket = bucket;
    }
}

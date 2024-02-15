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
    [SerializeField] private DragItemsNew dragItem;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startLerp = false;
        cut = false;
    }

    private void Update()
    {
        if (startLerp)
        {
            transform.position = Vector2.Lerp(transform.position, bucket.transform.position, Time.deltaTime * velocity);
            if (transform.position.y > bucket.transform.position.y - 0.02 && transform.position.y < bucket.transform.position.y + 0.02)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        if (cut)
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

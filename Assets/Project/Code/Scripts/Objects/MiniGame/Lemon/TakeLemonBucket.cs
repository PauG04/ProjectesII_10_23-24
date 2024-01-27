using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeLemonBucket : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector3 startPosition;

    private bool startLerp;
    private bool cut;

    [Header("Magnitude")]
    [SerializeField] private float maxMagnitude;

    [Header("Bucket Object")]
    [SerializeField] private GameObject bucket;
    [SerializeField] private float velocity;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnMouseDown()
    {
        if(cut)
            startLerp = true;
    }

    public void SetStartPostion()
    {
        cut = true;
        startPosition = transform.position;
    }
}

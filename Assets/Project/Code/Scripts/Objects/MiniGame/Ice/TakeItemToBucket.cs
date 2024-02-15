using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemToBucket : MonoBehaviour
{
    private Vector2 initPosition;
    private Rigidbody2D rb2d;
    private Vector3 startPosition;
    private Vector3 initScale;

    private bool startLerp;

    [Header("Magnitude")]
    [SerializeField] private float maxMagnitude;

    [Header("Bucket Object")]
    [SerializeField] private float velocity;
    private GameObject bucket;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initPosition = transform.localPosition;
        startPosition = transform.position;
        initScale = transform.localScale;
        startLerp = false;
    }

    private void Update()
    {
        if(startLerp)
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
        startLerp = true;
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    public void SetBucket(GameObject _bucket)
    {
        bucket = _bucket;
    }
}

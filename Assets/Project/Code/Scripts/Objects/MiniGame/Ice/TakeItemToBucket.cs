using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemToBucket : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool startLerp;

    [Header("Magnitude")]
    [SerializeField] private float maxMagnitude;

    [Header("Bucket Object")]
    [SerializeField] private float velocity;
    private GameObject bucket;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    private bool isDragging;
    private Vector3 offset;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startLerp = false;
    }

    private void Update()
    {
        if(isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
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
        isDragging = true;
        rb2d.bodyType = RigidbodyType2D.Static;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void SetBucket(GameObject _bucket)
    {
        bucket = _bucket;
    }
}

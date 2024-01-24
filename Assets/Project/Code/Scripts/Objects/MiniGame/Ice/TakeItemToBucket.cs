using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemToBucket : MonoBehaviour
{
    private Vector2 initPosition;
    private Rigidbody2D rigidbody2D;
    private Vector3 startPosition;
    private Vector3 initScale;

    private bool startLerp;

    [Header("Magnitude")]
    [SerializeField] private float maxMagnitude;

    [Header("Bucket Object")]
    [SerializeField] private GameObject bucket;
    [SerializeField] private float velocity;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    private void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
        initPosition = transform.localPosition;
        startPosition = transform.position;
        initScale = transform.localScale;
        startLerp = false;
    }

    private void Update()
    {
        stopObject();
        if(startLerp)
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
        if (transform.position.x > startPosition.x + Random.Range(maxMagnitude/2, maxMagnitude) || transform.position.x < startPosition.x - Random.Range(maxMagnitude / 2, maxMagnitude))
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        if (transform.position.y > startPosition.y + Random.Range(maxMagnitude / 2, maxMagnitude) || transform.position.y < startPosition.y - Random.Range(maxMagnitude / 2, maxMagnitude))
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnMouseDown()
    {
        startLerp = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && transform.localScale.magnitude <= initScale.magnitude * increaseScale - 1)
        {
            transform.localScale *= increaseScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace"))
        {
            transform.localScale = initScale;
        }
    }
}

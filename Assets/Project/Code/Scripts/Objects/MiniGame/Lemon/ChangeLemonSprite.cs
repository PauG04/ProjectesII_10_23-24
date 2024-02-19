using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLemonSprite : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Sprite lemonSlice;
    private Rigidbody2D rb2D;

    private bool startYLerp;

    [Header("Velocity")]
    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        rb2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(startYLerp)
        {
            LerpRotation();
        }
        
    }

    private void LerpRotation()
    {
        Quaternion newRotation = transform.rotation;
        newRotation.y = Mathf.Lerp(transform.rotation.y, 1, Time.deltaTime * rotationSpeed);
        newRotation.x = 0;
        transform.rotation = newRotation;

        if(transform.rotation.y > 0.99)
        {
            rb2D.bodyType = RigidbodyType2D.Static;
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<DragItemsNew>().enabled = true;
            startYLerp = false;
        }
    }

    public void SetLerp(bool state)
    {
        startYLerp = state;
    }
}

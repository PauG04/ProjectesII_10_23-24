using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLerp : MonoBehaviour
{
    [Header("Max Y Scale")]
    [SerializeField] private float maxYScale;
    private float initScale;

    [Header("Velocity")]
    [SerializeField] private float velocity;

    private bool isLerping;
    private bool isTotallyOpen;

    private void Awake()
    {
        initScale = transform.parent.localScale.y;

        isLerping = false;
        isTotallyOpen = false;
    }

    private void Update()
    {
        if(isLerping)
        {
            Lerping();
        }
    }

    private void OnMouseDown()
    {
        isLerping = true;
    }

    private void Lerping()
    {
        if(isTotallyOpen)
        {
            Vector3 newLocalScale = transform.parent.localScale;
            newLocalScale.y = Mathf.Lerp(transform.parent.localScale.y, initScale, Time.deltaTime * velocity);

            transform.parent.localScale = newLocalScale;

            if(transform.parent.localScale.y <= initScale + 0.2)
            {
                isLerping = false;
                isTotallyOpen = false;
            }
        }
        else
        {
            Vector3 newLocalScale = transform.parent.localScale;
            newLocalScale.y = Mathf.Lerp(transform.parent.localScale.y, maxYScale, Time.deltaTime * velocity);

            transform.parent.localScale = newLocalScale;

            if (transform.parent.localScale.y >= maxYScale - 0.2)
            {
                isLerping = false;
                isTotallyOpen = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopLerp : MonoBehaviour
{
    private bool canDoLerp;

    private void Start()
    {
        canDoLerp = true;  
    }

    private void Update()
    {
        if(canDoLerp)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(1.05f, 1.05f), 4 * Time.deltaTime);
            if (transform.localScale.y > 1-0.001)
                canDoLerp = false;
        }
    }
}

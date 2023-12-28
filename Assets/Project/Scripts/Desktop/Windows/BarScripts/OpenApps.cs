using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class OpenApps : MonoBehaviour
{
    [SerializeField] private GameObject bar;

    private bool isOpen;
    private bool startLerp;
    private float positionX;
    private float originalBarScale;

    private void Start()
    {
        isOpen = false;
        startLerp = false;
        positionX = transform.localPosition.x;
        originalBarScale = bar.GetComponentInChildren<SpriteRenderer>().localBounds.size.x;
    }

    private void Update()
    {
        if (!isOpen)
        {
            transform.localPosition = new Vector2(positionX + originalBarScale - 0.01f - bar.GetComponentInChildren<SpriteRenderer>().localBounds.size.x, transform.localPosition.y);
        }    
        else
            transform.localPosition = new Vector2(positionX - bar.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2, transform.localPosition.y);
    }

    private void OnMouseDown()
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
        startLerp = true;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public bool GetStartLerp()
    {
        return startLerp;
    }

    public void SetStartLoop(bool loop)
    {
        startLerp = loop;
    }


}

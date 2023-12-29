using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class OpenApps : MonoBehaviour
{
    private bool isOpen;
    private bool startLerp;

    private void Start()
    {
        isOpen = false;
        startLerp = false;
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

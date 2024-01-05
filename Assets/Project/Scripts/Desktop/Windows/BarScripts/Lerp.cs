using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    [SerializeField] private Vector2 endScale;
    [SerializeField] private GameObject[] apps;
    [SerializeField] private OpenApps openApp;

    private Vector2 startScale;
    

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        if (openApp.GetIsOpen() && openApp.GetStartLerp())
        {
            GrowBar();
        }
        if (!openApp.GetIsOpen() && openApp.GetStartLerp())
        {
            MinimizeBar();
            for (int i = 0; i < apps.Length; i++)
            {
                apps[i].SetActive(false);
            }
        }
    }

    private void GrowBar()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, endScale, Time.deltaTime * 15);
        if(transform.localScale.x > endScale.x - 0.01) 
        {
            openApp.SetStartLoop(false);
            for (int i = 0; i < apps.Length; i++)
            {
                apps[i].SetActive(true);
            }
        }
    }

    private void MinimizeBar()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * 15);
        if (transform.localScale.x < startScale.x + 0.01)
        {
            openApp.SetStartLoop(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSmashMesage : MonoBehaviour
{
    [SerializeField] private DragHammer _isPressing;

    private Vector3 shakeScale;
    private float timer;
    private float maxTimer = 2;
    private bool showMesage;
    private bool isFirtTime;

    private void Start()
    {
        isFirtTime = true;
        showMesage = false;
        shakeScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
    }
    private void Update()
    {
        StartCliking();
        ShowMesage();
        MesageTimer();
        
    }

    private void ShowMesage()
    {
        if (showMesage)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, shakeScale, 3 * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
        }
    }

    private void MesageTimer()
    {
        if (transform.localScale.y >= shakeScale.y - 0.2)
        {
            timer += Time.deltaTime;

            if (timer >= maxTimer)
            {
                showMesage = false;
                isFirtTime = false;
                timer = 0;
            }
        }
    }

    private void StartCliking()
    {
        if (_isPressing.GetDragging() && isFirtTime)
        {
            showMesage = true;
        }
        else if (!_isPressing.GetDragging())
        {
            showMesage = false;
            isFirtTime = true;
        }

    }
}

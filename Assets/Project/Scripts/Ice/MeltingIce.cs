using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingIce : MonoBehaviour
{
    [SerializeField] private float maxTimer;

    private Vector3 startScale;
    private Vector3 currentScale;
    private Vector3 meltScale;
    private float time;
    private bool startMelting;
   

    private void Start()
    {
        startScale= transform.localScale;
        currentScale = startScale / 5;
        meltScale = currentScale * 4;
    }

    private void Update()
    {
        IsIceMelted();
        if (!startMelting)
        {
            StartTimer();
        }
        else
        {
            MeltIce();
        }
            
    }

    private void StartTimer()
    {
        time += Time.deltaTime;

        if (time >= maxTimer)
        {
            time = 0;
            startMelting = true;
        }
    }

    private void MeltIce()
    {
        if (transform.localScale.y <= meltScale.y + 0.1)
        {
            startMelting = false;
            meltScale -= currentScale;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, meltScale, 1 * Time.deltaTime);
        }           
    }

    private void IsIceMelted()
    {
        if(transform.localScale.y <= 0)
        {
            Destroy(gameObject);
        }
    }

}

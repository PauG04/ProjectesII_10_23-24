using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRun : MonoBehaviour
{
    [SerializeField] private float velocity;
    private bool startRunning = false;

    private float currentY = -0.5f;

    private void Update()
    {
        if(startRunning && transform.localPosition.x < -2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + velocity, currentY, -1);
        }
        if(transform.localPosition.x > -2)
        {
            Destroy(gameObject);
        }
    }

    public void SetRunning(bool state)
    {
        startRunning = state;
    }

    public void SetCurrentY(float y) 
    {
        currentY = y;
    }
}

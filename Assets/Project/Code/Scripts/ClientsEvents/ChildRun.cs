using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRun : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    private bool startRunning = false;
    private bool isUp = false;

    private float currentY = -0.5f;

    private void Update()
    {
        MoveClientVertical();
        if (startRunning && transform.localPosition.x < -2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + velocity, currentY, -1);
        }
        if(transform.localPosition.x > -2)
        {
            Destroy(gameObject);
        }
    }

    private void MoveClientVertical()
    {
        Vector3 newPosition = transform.localPosition;
        if (isUp)
        {
            newPosition.y = Mathf.Lerp(transform.localPosition.y, maxY, Time.deltaTime * ClientManager.instance.GetHorizontalVelocity());
            if (newPosition.y >= maxY - 0.01f)
            {
                isUp = false;
            }
        }
        else
        {
            newPosition.y = Mathf.Lerp(transform.localPosition.y, minY, Time.deltaTime * ClientManager.instance.GetHorizontalVelocity());
            if (newPosition.y <= minY + 0.01f)
            {
                isUp = true;
            }
        }
        transform.localPosition = newPosition;
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

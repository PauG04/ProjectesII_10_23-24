using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRow : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    [SerializeField] private bool rowStopped;
    [SerializeField] private string stoppedSlot;
    private void Start()
    {
        rowStopped = true;
        SlotController.SpinSlot += StartRotating;
    }
    public void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        RectTransform rectTransform= GetComponent<RectTransform>();

        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 20; i++)
        {
            FixRow(rectTransform);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(40, 80);

        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3;
                break;
            case 2:
                randomValue += 2; 
                break;
            case 3:
                randomValue += 1;
                break;
        }

        // Slow down the spin 
        for (int i = 0; i < randomValue; i++)
        {
            FixRow(rectTransform);

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.025f;
            if (i > Mathf.RoundToInt(randomValue * 0.50f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.075f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.1f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (rectTransform.anchoredPosition.y <= -20f && rectTransform.anchoredPosition.y >= -30f)
            stoppedSlot = "Bar";
        else if (rectTransform.anchoredPosition.y <= -70f && rectTransform.anchoredPosition.y >= -80f)
            stoppedSlot = "Beer";
        else
            stoppedSlot = "Drinks";

        rowStopped = true;
    }

    private void OnDestroy()
    {
        SlotController.SpinSlot -= StartRotating;
    }

    private void FixRow(RectTransform rectTransform)
    {
        if (rectTransform.anchoredPosition.y <= -175f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 175f);
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 12.5f);
    }

    public bool GetRowStopped()
    {
        return rowStopped;
    }
    public string GetStoppedSlot()
    {
        return stoppedSlot;
    }
}

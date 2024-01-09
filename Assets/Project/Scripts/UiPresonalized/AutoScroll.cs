using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    private RectTransform rectTrasform;
    private float previousHeight;

    private void Awake()
    {
        rectTrasform = GetComponent<RectTransform>();
        previousHeight = rectTrasform.sizeDelta.y;

    }

    private void Update()
    {
        if (rectTrasform != null)
        {
            if (previousHeight < rectTrasform.sizeDelta.y)
            {
                rectTrasform.anchoredPosition = new Vector2(0, rectTrasform.sizeDelta.y);
                previousHeight = rectTrasform.sizeDelta.y;
            }
        }
    }
}

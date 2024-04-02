using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyNextSprite : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private float positionOffsetY;
    private float temporalPositionY;
    private RectTransform rectTransform;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        positionOffsetY = rectTransform.anchoredPosition.y;
    }

    private void Update()
    {
        temporalPositionY = positionOffsetY;
        temporalPositionY += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, temporalPositionY);
    }
}

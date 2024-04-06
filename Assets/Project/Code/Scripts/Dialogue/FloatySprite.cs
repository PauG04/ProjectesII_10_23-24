using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySprite : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private float positionOffsetY;
    private float positionOffsetX;

    private float temporalPositionY;
    private float temporalPositionX;

    private RectTransform rectTransform;

    [Header("OptionsValue")]
    [SerializeField] private bool isRectTransform;
    [SerializeField] private bool moveVertical;

    private void Start()
    {
        if (isRectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
            positionOffsetY = rectTransform.anchoredPosition.y;
            positionOffsetX = rectTransform.anchoredPosition.x;
        }
        else
        {
            positionOffsetY = transform.position.y;
            positionOffsetX = transform.position.x;
        }

    }
    private void Update()
    {
        if (isRectTransform)
        {
            if (moveVertical)
            {
                temporalPositionY = positionOffsetY;
                temporalPositionY += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, temporalPositionY);
            }
            else
            {
                temporalPositionX = positionOffsetX;
                temporalPositionX += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                rectTransform.anchoredPosition = new Vector2(temporalPositionX, rectTransform.anchoredPosition.y);
            }
        }
        else
        {
            if (moveVertical)
            {
                temporalPositionY = positionOffsetY;
                temporalPositionY += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                transform.position = new Vector2(transform.position.x, temporalPositionY);
            }
            else
            {
                temporalPositionX = positionOffsetX;
                temporalPositionX += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                transform.position = new Vector2(temporalPositionX, transform.position.y);
            }
        }
    }
}

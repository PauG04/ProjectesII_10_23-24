using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private GameObject liquidSlider;
    [SerializeField] private GameObject arrowSlider;
    private GameObject arrow;
    private GameObject arrowObject;
    private float maxYScale;
    private float maxYPosition;

    [Header("LiquidManager")]
    [SerializeField] private LiquidManager liquidManager;

    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sprite;


    private void Awake()
    {
        float widht;
        float height;
        float arrowHeight;

        SpriteRenderer spriteRenderer;

        if (sprite != null)
        {
             spriteRenderer = sprite;
        }
        else
        {
             spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        widht = spriteRenderer.bounds.size.x;
        height= spriteRenderer.bounds.size.y;

        arrow = Instantiate(liquidSlider, transform);
        arrowObject = Instantiate(arrowSlider, transform);
        arrow.transform.position = new Vector2(transform.position.x + widht/1.5f, transform.position.y - height/2);

        arrowHeight = arrow.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
 
        maxYScale = height / arrowHeight;
        maxYPosition = arrow.transform.localPosition.y + height;   
    }

    private void Update()
    {
        float currentYScale = (maxYScale * liquidManager.GetCurrentLiquid()) / liquidManager.GetMaxLiquid();
        arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, currentYScale, arrow.transform.localScale.z);

        float currentYPosition = (maxYPosition * liquidManager.GetCurrentLiquid() * 2) / liquidManager.GetMaxLiquid();
        arrowObject.transform.localPosition = new Vector2(arrow.transform.localPosition.x, currentYPosition - maxYPosition);
    }
}

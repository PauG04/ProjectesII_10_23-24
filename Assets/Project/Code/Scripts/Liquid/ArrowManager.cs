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

    [Header("Boolean")]
    [SerializeField] private bool isHorizontal;


    private void Awake()
    {
        float widht;
        float height;
        float arrowHeight;
        float arrowWidth;

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
        height = spriteRenderer.bounds.size.y;

        arrow = Instantiate(liquidSlider, transform);
        arrowObject = Instantiate(arrowSlider, transform);
        

        arrowHeight = arrow.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        arrowWidth = arrow.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        if (!isHorizontal)
        {
            arrow.transform.position = new Vector2(transform.position.x + widht / 1.5f, transform.position.y - height / 2);
            maxYScale = height / arrowHeight;
            maxYPosition = arrow.transform.localPosition.y + height;
           
        }
        else
        {
            arrow.transform.position = new Vector2(transform.position.x - widht / 1.5f, transform.position.y - height / 2);
            maxYScale = widht / arrowWidth;
            maxYPosition = arrow.transform.localPosition.x + height;
            
        }

    }

    private void Update()
    {
        float currentYScale = (maxYScale * liquidManager.GetCurrentLiquid()) / liquidManager.GetMaxLiquid();        
        float currentYPosition = (maxYPosition * liquidManager.GetCurrentLiquid() * 2) / liquidManager.GetMaxLiquid();

        if(!isHorizontal)
        {
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, currentYScale, arrow.transform.localScale.z);
            arrowObject.transform.localPosition = new Vector2(arrow.transform.localPosition.x, currentYPosition - maxYPosition); 
        }
        else
        {
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, currentYScale, arrow.transform.localScale.z);
            arrowObject.transform.localPosition = new Vector2(-arrow.transform.localPosition.x, currentYPosition - maxYPosition);
        }
        
     
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanLiquid : MonoBehaviour
{
    [Header("Saturation")]
    [SerializeField] private float difference;
    [SerializeField] private float maxSaturation;
    [SerializeField] private float cleanMopDifference;

    private DragItemsNew dragItemsNew;
    private bool cleanMop;

    [Header("Clean Time")]
    [SerializeField] private float cleanTime;
    private float timer;

    private Color _color;

    private void Awake()
    {
        _color = GetComponent<SpriteRenderer>().color;

        dragItemsNew = GetComponent<DragItemsNew>();
    }

    private void Update()
    {     
        CleanMop();
    }

    private void CleanMop()
    {
        if (Input.GetMouseButtonDown(1) && dragItemsNew.GetIsDraggin() && !cleanMop)
        {
            cleanMop = true;
            
        }
        if(cleanMop)
        {
            timer += Time.deltaTime;
            if(timer > cleanTime) 
            {
                float differenceColor = (1 - _color.r) / cleanMopDifference;
                _color = new Color(_color.r + differenceColor, _color.g + differenceColor, _color.b + differenceColor);
                GetComponent<SpriteRenderer>().color = _color;
                timer = 0;
                cleanMop= false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Liquid") && !cleanMop)
        {           
            if(_color.g > maxSaturation)
            {
                Destroy(collision.gameObject);
                _color = new Color(_color.r - difference, _color.g - difference, _color.b - difference);
                GetComponent<SpriteRenderer>().color = _color;
            }           
        }
    }
}

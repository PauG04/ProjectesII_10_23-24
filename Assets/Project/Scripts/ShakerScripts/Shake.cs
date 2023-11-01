using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    [SerializeField]
    private float progress = 0.0f;
    Vector2 shakerPosition;
    Vector2 newShakerPosition;
    private bool shaking = false;
    [SerializeField]
    private float minimizeBarProgress;
    [SerializeField]
    private bool isShakingDown;
    private Drink shaker;
    private PolygonCollider2D polygonCollider2D;
    private GameObject sprite;
    [SerializeField]
    private GameObject[] sliders;
    private int currentBox = 0;
    private float maxValue = 2;
    private float value;

    private void Start()
    {
        shaker = GetComponent<Drink>();
        sprite = this.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        sprite = sprite.transform.GetChild(0).gameObject;
        polygonCollider2D= sprite.GetComponent<PolygonCollider2D>();
        value = maxValue / 10;
    }

    private void Update()
    {
        StartClicking();
        EndClicking();
        if (shaking && progress <= maxValue)
        {
            DirectionShaker();
            IncreaseBar();
            SetVector();
            //SetShakerStata();
            ActiveSlider();
        }    
    }
    public void StartClicking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (polygonCollider2D.OverlapPoint(mousePosition))
            {
                shaking = true;
                shakerPosition = new Vector2(transform.position.x, transform.position.y);
            }
            else
            {
                shaking = false;
            }
        }
    }

    public void EndClicking()
    {
        if (Input.GetMouseButtonUp(0))
        {
            shaking = false;
        }
    }

    private void OnMouseDown()
    {
        shaking = true;
    }

    private void OnMouseUp()
    {
        shaking = false; 
    }

    private void DirectionShaker()
    {
        newShakerPosition = new Vector2(transform.position.x, transform.position.y);
        if (shakerPosition.y >= newShakerPosition.y)
        {
            isShakingDown = true;
        }         
        else if(shakerPosition.y <= newShakerPosition.y)
        {
            isShakingDown = false;
        }
           
    }
    private void SetVector()
    {
          shakerPosition = new Vector2(transform.position.x, transform.position.y);
    }
    private void IncreaseBar()
    {
        if(isShakingDown)
            progress += (shakerPosition.y - newShakerPosition.y) / minimizeBarProgress;
        else
            progress += -(shakerPosition.y - newShakerPosition.y) / minimizeBarProgress;
    }

    private void ActiveSlider()
    {
        if(progress >= value)
        {
            sliders[currentBox].SetActive(true);
            currentBox++;
            value += maxValue / 10;
        }
    }

    /*
    private void SetShakerStata()
    {
       if (progress >= maxValue)
       {
           shaker.SetDrinkState(Drink.DrinkState.Shaked);
       }
       else if (progress > (maxValue / 2) && progress < maxValue)
       {
           shaker.SetDrinkState(Drink.DrinkState.Mixed);
       }

       else
       {
           shaker.SetDrinkState(Drink.DrinkState.Idle);
       }
   
    }
    */
}

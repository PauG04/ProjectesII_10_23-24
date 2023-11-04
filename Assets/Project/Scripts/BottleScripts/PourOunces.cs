using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Drink;

public class PourOunces : MonoBehaviour
{
    private Bottle bottle;
    private SpriteRenderer bottlerRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    //[SerializeField]
    //private OuncesCounter ouncesCounter;
    [Header("Shaker")]
    [SerializeField] private bool isShaker;
    private GameObject shaker;
    [SerializeField] private Drink drinkInsideShaker;
    [SerializeField] private BoxCollider2D shakerCollider;

    [Header("Result")]
    private GameObject result;
    [SerializeField] private ResultDrink resultDrink;
    [SerializeField] private BoxCollider2D resultCollider;

    private Shake shake;
    private CloseShaker close;
    private GameObject closeButton;


    private void Awake()
    {
        if (!isShaker)
        {
            bottle = GetComponent<Bottle>();
            bottlerRenderer = bottle.GetComponent<SpriteRenderer>();
        }
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void OnMouseDown()
    {
        if(GameObject.Find("Shaker") != null) 
        {
            shaker = GameObject.Find("Shaker");
            shakerCollider = shaker.GetComponent<BoxCollider2D>();
            drinkInsideShaker = shaker.GetComponent<Drink>();
            shake= shaker.GetComponent<Shake>();
        }
        if(GameObject.Find("Result") != null)
        {
            result = GameObject.Find("Result");
            resultCollider = result.GetComponent<BoxCollider2D>();
            resultDrink = result.GetComponent<ResultDrink>();
        }
        if(GameObject.Find("CloseShaker") != null)
        {
            closeButton = GameObject.Find("CloseShaker");
            close = closeButton.GetComponent<CloseShaker>();
        }
            
    }

    private void OnMouseUp()
    {
        if (shaker != null && !close.GetClose())
        {
            if (!isShaker && shake.GetProgres() == 0)
            {
                if (boxCollider.IsTouching(shakerCollider) && bottle.GetCurrentOunces() > 0)
                {
                    drinkInsideShaker.AddOunce(bottle.GetTypeOfOunces()[0]);
                    shake.GetSprite().color = bottlerRenderer.color;
                    shake.SetIndex();
                    bottle.SubstractOneOunce();

                }
            }

            if (result != null) 
            {
                if (boxCollider.IsTouching(resultCollider))
                {
                    resultDrink.shakerResult.Clear();
                    foreach (TypeOfDrink drink in drinkInsideShaker.GetTypeOfOunces())
                    {
                        resultDrink.shakerResult.Add(drink);
                    }
                    resultDrink.shake = drinkInsideShaker.GetDrinkState();
                }
            }
        }
    }
}

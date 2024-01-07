using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour
{
    [SerializeField] private DrinkTaken drinkTaken;
    [SerializeField] private float maxTime;
    [SerializeField] private int minTimeBetweenOrder;
    [SerializeField] private int maxTimeBetweenOrder;

    private bool isWorking = true; //esta variable se activa y se desactiva cuando se abre la app de trabajo
    private bool canTakeOrder = false;
    private float currentTime = 0.0f;


    private void Start()
    {
        maxTime = Random.Range(minTimeBetweenOrder, maxTimeBetweenOrder);
    }
    void Update()
    {
        if (isWorking && canTakeOrder)
        {
            TakeOrder();
        }
        else if (isWorking && drinkTaken.GetIsDrinkPrepares())
        {
            Timer();
        }
    }

    private void TakeOrder()
    {
        drinkTaken.SetIsDrinkPrepares(false);
        canTakeOrder = false;
        Debug.Log("I want a Beer");
    }

    private void Timer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            canTakeOrder = true;
            currentTime = 0.0f;
            maxTime = Random.Range(minTimeBetweenOrder, maxTimeBetweenOrder);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Drink;

public class ResultDrink : MonoBehaviour
{

    [Header("Shaker")]
    public List<TypeOfDrink> shakerResult;

    private void Awake()
    {
        shakerResult = new List<TypeOfDrink>();
    }

    private void Update()
    {

    }
}

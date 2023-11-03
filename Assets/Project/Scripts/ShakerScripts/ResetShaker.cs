using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShaker : MonoBehaviour
{
    [SerializeField] private Drink drinksInsideShaker;
    [SerializeField] private Shake shake;
    private void OnMouseDown()
    {
        drinksInsideShaker.ResetDrinks();
        shake.ResetShaker();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShaker : MonoBehaviour
{
    [SerializeField] private LiquidManager drinksInsideShaker;
	[SerializeField] private ShakerController shakeController;
    [SerializeField] private ProgressSlider slider;
    private void OnMouseDown()
    {
        drinksInsideShaker.ResetDrink();
        shakeController.ResetShaker();
        slider.ResetSlider();
        Debug.Log("Reset Drink");
    }
}

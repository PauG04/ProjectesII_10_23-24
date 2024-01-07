using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShaker : MonoBehaviour
{
    [SerializeField] private LiquidManager drinksInsideShaker;
    [SerializeField] private ProgressSlider slider;
    private void OnMouseDown()
    {
	    drinksInsideShaker.ResetDrink();
	    slider.ResetSlider();
    }
}

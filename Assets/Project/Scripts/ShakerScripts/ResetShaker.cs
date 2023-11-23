using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShaker : MonoBehaviour
{
    [SerializeField] private Drink drinksInsideShaker;
    [SerializeField] private Shaker shake;
    [SerializeField] private ProgressSlider slider;
    private void OnMouseDown()
    {
        drinksInsideShaker.ResetDrinks();
        shake.ResetShaker();
        slider.ResetSlider();
    }
}

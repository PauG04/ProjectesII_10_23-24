using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDrink : MonoBehaviour
{
    public bool reset;
    [SerializeField] private ResultDrink result;
    private void OnMouseDown()
    {
        result.drinksInside.Clear();
        result.SetText("");
        result.SetEnabledSprite(false);
    }
}

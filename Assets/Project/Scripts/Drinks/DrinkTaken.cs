using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTaken : MonoBehaviour
{
    private bool isDrinkPrepares = true;
    [SerializeField]
    private DrinkScript drinkScript;
    [SerializeField]
    private GameObject cup;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Drink" && drinkScript.GetIsMouseNotPressed())
        {
            cup.SetActive(false);
            isDrinkPrepares = true;
            Debug.Log("SI");
        }
    }

    public void SetIsDrinkPrepares(bool ispPrepare)
    {
        isDrinkPrepares= ispPrepare;
    }

    public bool GetIsDrinkPrepares()
    {
        return isDrinkPrepares;
    }

}

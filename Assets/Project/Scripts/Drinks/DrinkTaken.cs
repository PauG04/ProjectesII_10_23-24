using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTaken : MonoBehaviour
{
    private bool isDrinkPrepares = true;
    [SerializeField] private ShakerController shakerController;
    [SerializeField] private GameObject glass;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Drink" && shakerController.GetIsMousePressed())
        {
            glass.SetActive(false);
            isDrinkPrepares = true;
            Debug.Log("Si");
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

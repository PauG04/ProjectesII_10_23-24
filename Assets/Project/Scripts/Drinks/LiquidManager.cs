using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private List<Drink.TypeOfDrink> typeOfDrinkInside;
    private int numberOfParticles = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
            numberOfParticles++;
        }
    }
}
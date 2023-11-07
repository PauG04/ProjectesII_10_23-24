using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private List<TypeOfCocktail> typeOfDrinkList;
    [SerializeField] private TypeOfCocktail drinkThatWants;
    [SerializeField] public TypeOfCocktail drinkDropped;

    private void Awake()
    {
        RandomDrinkChose();
    }

    private void Update()
    {
        CheckDrink();
    }

    private void RandomDrinkChose()
    {
        drinkThatWants = typeOfDrinkList[Random.Range(0, typeOfDrinkList.Count - 1)];
        textMeshPro.text = "I want a " + drinkThatWants.ToString();
    }
    private void CheckDrink()
    {
        if (drinkDropped == TypeOfCocktail.Empty)
        {
            textMeshPro.text = "I want a " + drinkThatWants.ToString();
        }
        else if (drinkDropped == drinkThatWants)
        {
            textMeshPro.text = "That drink was delicious!";
        }
        else if(drinkDropped != drinkThatWants)
        {
            textMeshPro.text = "What a piece of shit";
        }
    }
}

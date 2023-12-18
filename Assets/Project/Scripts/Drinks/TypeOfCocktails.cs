using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cocktail", menuName = "Cocktail", order = 3)]
public class TypeOfCocktails
{
    public enum TypeOfCocktail
    {
        LondonMist,
        CambridgeBreeze,
        BristolBloom,
        ManchesterMule
    }

    public TypeOfCocktail typeOfCocktail;
    public List<TypeOfDrinks> typeOfDrinksNeeded;
}
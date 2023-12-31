using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cocktail", menuName = "Cocktail", order = 3)]
public class TypeOfCocktails : ScriptableObject
{
	[System.Serializable]
	public class DrinkQuantity
	{
		public TypeOfDrinks.TypeOfDrink type;
		public int quantity;
	}
	
    public enum TypeOfCocktail
    {
        LondonMist,
        CambridgeBreeze,
        BristolBloom,
        ManchesterMule,
        Sekiro
    }

    public TypeOfCocktail typeOfCocktail;
	public List<DrinkQuantity> typeOfDrinksNeeded;
    public float price;
    public string subtitle;
    public string description;
    public Sprite sprite;
}
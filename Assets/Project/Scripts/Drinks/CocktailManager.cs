using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailManager : MonoBehaviour
{
	public List<TypeOfCocktails> availableCocktails;

	public void CreateCocktail()
	{
		foreach (TypeOfCocktails cocktail in availableCocktails)
		{
			if (IngredientsMatch(cocktail))
			{
				Debug.Log("Cocktail Created: " + cocktail.typeOfCocktail);
			}
		}
	}

	private bool IngredientsMatch(TypeOfCocktails cocktail)
	{
		foreach (TypeOfCocktails.DrinkQuantity requiredIngredient in cocktail.typeOfDrinksNeeded)
		{
			
		}
		/*
		foreach (TypeOfCocktails.DrinkQuantity requiredIngredient in cocktail.DrinkQuantity)
		{
			
			
			// ingredientQuantities is the dictionary in the liquid storage
			
			if (!ingredientQuantities.ContainsKey(requiredIngredient.type) || ingredientQuantities[requiredIngredient.type] < requiredIngredient.quantity)
			{
				return false;
			}
			
		}*/
		return true;
	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "Drink", order = 2)]
public class TypeOfDrinks : ScriptableObject
{
    public enum TypeOfDrink
    {
        //Alcoholic Drinks
        GlacierSpirit,
        RusticGold,
        DesertRose,
        HerbHaven,
        Tequila,
        //Juices
        OrangeJuice,
        LemonJuice,
        //Soft Drinks
        Cola,
        Soda,
        Tonic
    }

    //public string drinkName;
    public string description;
    public TypeOfDrink type;
    public Color color;
    [Space(10)]
    public bool hasAlcohol;
    public bool isBubbly;
}

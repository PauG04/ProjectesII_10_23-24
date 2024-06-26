using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "Drink", order = 2)]
public class DrinkNode : ScriptableObject
{
    public enum Type
    {
        //Alcoholic Drinks
        Rum,
        Vodka,
        Gin,
        Whiskey,
        Wine,

        //Juices
        OrangeJuice,
        LemonJuice,

        //Soft Drinks
        Cola,
        Soda,
        Tonic,

        //Beer
        Beer
    }

    public string spanishName;
    public string description;
    public Type type;
    public Color color;
    [Space(10)]
    public bool hasAlcohol;
    public bool isBubbly;
}

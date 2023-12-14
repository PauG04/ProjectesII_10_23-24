using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using static Drink;
using UnityEditor;

public class ResultDrink : MonoBehaviour
{

    [Header("Drinks List")]
    public List<TypeOfDrink> drinksInside;
    private bool listChanged;

    [Header("Result")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite censoredDrink;
    [SerializeField] private TextMeshPro textMeshPro;
    private TypeOfCocktail result;

    [Header("Shaker")]
    private DrinkState shakeState;
    private int shakerPreviousDrinks;

    private StateManager stateManager;

    #region Drinks
    private int rum;
    private int gin;
    private int vodka;
    private int whiskey;
    private int tequila;
    private int orangeJuice;
    private int lemonJuice;
    private int cola;
    private int soda;
    private int tonic;
    #endregion

    private void Awake()
    {
        spriteRenderer.enabled = false;
    }
    private void Update()
    {
        listChanged = shakerPreviousDrinks != drinksInside.Count();

        if (listChanged && drinksInside.Count() > 0)
        {
            spriteRenderer.enabled = true;
            StartCoroutine(ResultAnimation());
            UpdateDrinks();
            MakeResult();
        }
    }
    private void MakeResult()
    {
        if (shakeState == DrinkState.Idle)
        {
            if (gin == 2 && tonic == 4)
            {
                result = TypeOfCocktail.GinTonic;
            }
            else if (rum == 2 && cola == 4)
            {
                result = TypeOfCocktail.RonCola;
            }
            else if (whiskey == 4)
            {
                result = TypeOfCocktail.OldFashioned;
            }
            else
            {
                result = TypeOfCocktail.Mierdon;
            }
        }
        if (shakeState == DrinkState.Mixed)
        {
            if (rum == 2 && lemonJuice == 2 && soda == 4)
            {
                result = TypeOfCocktail.Mojito;
            }
            else if (tequila == 2 && orangeJuice == 2)
            {
                result = TypeOfCocktail.TequilaSunrise;
            }
            else if (gin == 4 && lemonJuice == 2)
            {
                result = TypeOfCocktail.TomCollins;
            }
            else if (gin == 2 && rum == 2 && orangeJuice == 4 && lemonJuice == 1)
            {
                result = TypeOfCocktail.MaiTai;
            }
            else if (gin == 4 && lemonJuice == 4 && tonic == 1 && soda == 1)
            {
                result = TypeOfCocktail.CorpseReviver;
            }
            else
            {
                result = TypeOfCocktail.Mierdon;
            }
        }
        if (shakeState == DrinkState.Shaked)
        {
            if (tequila == 2 && lemonJuice == 2 && vodka == 2)
            {
                result = TypeOfCocktail.Margarita;
            }
            else if (vodka == 2 && lemonJuice == 2 && orangeJuice == 2)
            {
                result = TypeOfCocktail.SexOnTheBeach;
            }
            else if (orangeJuice == 4 && whiskey == 2)
            {
                result = TypeOfCocktail.FuzzyNavel;
            }
            else if (whiskey == 4 && lemonJuice == 2 && soda == 1)
            {
                result = TypeOfCocktail.WhiskeySour;
            }
            else if (vodka == 4 && lemonJuice == 3)
            {
                result = TypeOfCocktail.MoscowMule;
            }
            else if (gin == 2 && vodka == 2 && rum == 1 && lemonJuice == 2 && orangeJuice == 1 && soda == 2)
            {
                result = TypeOfCocktail.LastWord;
            }
            else
            {
                result = TypeOfCocktail.Mierdon;
            }
        }
    }
    private void UpdateDrinks()
    {
        rum = drinksInside.Where(drink => drink == TypeOfDrink.Rum).Count();
        gin = drinksInside.Where(drink => drink == TypeOfDrink.Gin).Count();
        vodka = drinksInside.Where(drink => drink == TypeOfDrink.Vodka).Count();
        whiskey = drinksInside.Where(drink => drink == TypeOfDrink.Whiskey).Count();
        tequila = drinksInside.Where(drink => drink == TypeOfDrink.Tequila).Count();
        orangeJuice = drinksInside.Where(drink => drink == TypeOfDrink.OrangeJuice).Count();
        lemonJuice = drinksInside.Where(drink => drink == TypeOfDrink.LemonJuice).Count();
        cola = drinksInside.Where(drink => drink == TypeOfDrink.Cola).Count();
        soda = drinksInside.Where(drink => drink == TypeOfDrink.Soda).Count();
        tonic = drinksInside.Where(drink => drink == TypeOfDrink.Tonic).Count();

        shakerPreviousDrinks = drinksInside.Count();
    }
    private IEnumerator ResultAnimation()
    {
        if (drinksInside.Count > 0)
        {
            for (int spriteIndex = 0; spriteIndex < sprites.Count; spriteIndex++)
            {
                spriteRenderer.sprite = sprites[spriteIndex];

                if (spriteIndex >= sprites.Count - 1)
                {
                    textMeshPro.text = result.ToString();

                    if (result == TypeOfCocktail.Mierdon)
                    {
                        spriteRenderer.sprite = censoredDrink;
                    }
                }

                stateManager.AddToState("fatigue", 0.5f);

                yield return new WaitForSeconds(0.4f);

                switch (spriteIndex)
                {
                    case 0:
                        AudioManager.instance.Play("result0");
                        break;
                    case 1:
                        AudioManager.instance.Play("result1");
                        break;
                    case 2:
                        AudioManager.instance.Play("result2");
                        break;
                    case 3:
                        AudioManager.instance.Play("result3");
                        break;
                    case 4:
                        if (result == TypeOfCocktail.Mierdon)
                        {
                            AudioManager.instance.Play("badResult");
                        }
                        else
                        {
                            AudioManager.instance.Play("goodResult");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void SetEnabledSprite(bool spriteEnabled)
    {
        spriteRenderer.enabled = spriteEnabled;
    }
    public void SetShakerStete(DrinkState shakeState)
    {
        this.shakeState = shakeState;
    }
    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
    public TypeOfCocktail GetResult()
    {
        return result;
    }
}

public enum TypeOfCocktail
{
    Empty,
    GinTonic,
    RonCola,
    OldFashioned,
    Mojito,
    Margarita,
    SexOnTheBeach,
    TequilaSunrise,
    FuzzyNavel,
    WhiskeySour,
    TomCollins,
    MoscowMule,
    LastWord,
    MaiTai,
    CorpseReviver,
    Mierdon
}
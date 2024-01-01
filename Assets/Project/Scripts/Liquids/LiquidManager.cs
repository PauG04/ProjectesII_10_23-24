using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LiquidManager : MonoBehaviour
{
    #region ENUMS
    public enum TypeOfCocktail
	{
		Mierdon,
	    GinTonic,
		RumCola,
	    Negroni
    }
    public enum TypeOfDrink
    {
        //Alcoholic Drinks
        Rum,
        Gin,
        Vodka,
        Whiskey,
        Tequila,
        //Juices
        OrangeJuice,
        LemonJuice,
        //Soft Drinks
        Cola,
        Soda,
        Tonic
    }
    
    public enum DrinkState
    {
        Idle,
        Shaked,
        Mixed
    }

    public enum DrinkCategory
    {
        Alcohol,
        Soda
    }
    #endregion

    [Header("Renderer Variables")]
    private DrinkState drinkState;
    [SerializeField] private Renderer fluidRenderer;
    [SerializeField] private int maxCapacity;
    
    private Dictionary<TypeOfDrink, int> typeOfDrinkInside;  

    private float fill;
    private int currentLayer;
    private int numberOfParticles = 0;

    [Header("Shaker Variables")]
    [SerializeField] private bool isShaker;
	[SerializeField] private ShakerController shakerController;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0.1475f;
    [SerializeField] private float minColliderPos = -0.23f;

	[Header("Cocktail")]
	public TypeOfCocktail typeOfCocktail;

    [Header("Slider")]
    [SerializeField] private float maxBar;
    private GameObject currentSlider;
    [SerializeField] private CreateSlider slider;
    private Color currentColor;
    private float nextSliderPositon;


    private void Awake()
    {
        typeOfDrinkInside = new Dictionary<LiquidManager.TypeOfDrink, int>();
        currentLayer = gameObject.layer;
        drinkState = DrinkState.Idle;
        currentColor = Color.clear;
        nextSliderPositon = 0;
    }
    private void Update()
    {
        if (isShaker)
        {
            FillShaker();
        }
        else
        {
            FillDrink();
        }
	    CreateCocktail();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid") && numberOfParticles < maxCapacity)
        {
            LiquidParticle particleCollision = collision.GetComponent<LiquidParticle>();

            if (!typeOfDrinkInside.ContainsKey(particleCollision.liquidType))
            {
                typeOfDrinkInside.Add(particleCollision.liquidType, 1);
            }
            else
            {
                typeOfDrinkInside[particleCollision.liquidType]++;
            }
	        //Debug.Log(particleCollision.liquidType + " has " + typeOfDrinkInside[particleCollision.liquidType] + " particles inside.");

            Destroy(collision.gameObject);     
            if(currentColor != collision.GetComponent<LiquidParticle>().color)
            {        
                currentSlider = slider.CreateCurrentSlider(nextSliderPositon);
                
                currentColor = collision.GetComponent<LiquidParticle>().color;
            }
            currentSlider.GetComponentInChildren<SpriteRenderer>().color = collision.GetComponent<LiquidParticle>().color;
            nextSliderPositon = currentSlider.transform.localPosition.y + currentSlider.GetComponentInChildren<SpriteRenderer>().bounds.size.y/1.5f;
            currentSlider.transform.localScale += new Vector3(0, (maxBar / maxCapacity), 0);
            numberOfParticles++;
        }
    }
    private void FillDrink()
    {
        fluidRenderer.material.SetFloat("_Fill", fill);

        if (numberOfParticles < maxCapacity)
        {
            fill = (float)numberOfParticles / maxCapacity;
            float colliderPosition = minColliderPos + (fill * (maxColliderPos - minColliderPos)) / 1;
            transform.localPosition = new Vector3(transform.localPosition.x, colliderPosition, transform.localPosition.z);
        }
    }
    private void FillShaker()
    {
        if (numberOfParticles < maxCapacity)
        {
            gameObject.layer = currentLayer;
        } 
        else
        {
            gameObject.layer = 0;
        }
    }
    public void ResetDrink()
    {
        typeOfDrinkInside.Clear();
        numberOfParticles = 0;
    }
    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }
	public void CreateCocktail()
	{
		if (typeOfDrinkInside.ContainsKey(TypeOfDrink.Gin) && typeOfDrinkInside.ContainsKey(TypeOfDrink.Tonic))
		{
			int ginCount = typeOfDrinkInside[TypeOfDrink.Gin];
			int tonicCount = typeOfDrinkInside[TypeOfDrink.Tonic];

			if (ginCount >= 25 && tonicCount >= 25 && drinkState == DrinkState.Idle)
			{
				typeOfCocktail = TypeOfCocktail.GinTonic;
			}
		}
		else if (typeOfDrinkInside.ContainsKey(TypeOfDrink.Rum) && typeOfDrinkInside.ContainsKey(TypeOfDrink.Cola))
		{
			int rumCount = typeOfDrinkInside[TypeOfDrink.Rum];
			int colaCount = typeOfDrinkInside[TypeOfDrink.Cola];

			if (rumCount >= 25 && colaCount >= 25 && drinkState == DrinkState.Shaked)
			{
				typeOfCocktail = TypeOfCocktail.RumCola;
			}
		}
		else 
		{
			typeOfCocktail = TypeOfCocktail.Mierdon;
		}
	}
	// Temporal, use later
	public Dictionary<TypeOfDrink, int> TraspassDrinks()
	{
		Dictionary<TypeOfDrink, int> newDictionary = new Dictionary<TypeOfDrink, int>(typeOfDrinkInside);
		
		foreach (TypeOfDrink drinkType in typeOfDrinkInside.Keys)
		{
			if (typeOfDrinkInside[drinkType] > 0)
			{
				newDictionary[drinkType] = typeOfDrinkInside[drinkType] - 1;
			}
		}
		
		typeOfDrinkInside = newDictionary;
		return typeOfDrinkInside;
	}
	
	
    public Dictionary<TypeOfDrink, int> GetTypeOfDrinkInside()
    {
        return typeOfDrinkInside;
    }
    public int GetMaxCapacity()
    {
        return maxCapacity;
    }
    public int GetCurrentLiquid()
    {
        return numberOfParticles;
    }
    public void DecreaseLiquid()
    {
        numberOfParticles--;
    }
    public void SetDrinkState(DrinkState _drinkState)
    {
        drinkState = _drinkState;
    }
}
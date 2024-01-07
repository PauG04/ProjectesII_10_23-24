using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LiquidManager : MonoBehaviour
{
    [Header("Renderer Variables")]
	private TypeOfCocktails.StateOfCocktail drinkState;
    [SerializeField] private Renderer fluidRenderer;
    [SerializeField] private int maxCapacity;
    
	private Dictionary<TypeOfDrinks.TypeOfDrink, int> typeOfDrinkInside;  

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
	public TypeOfCocktails.TypeOfCocktail typeOfCocktail;

    [Header("Slider")]
    [SerializeField] private float maxBar;
    private GameObject currentSlider;
    [SerializeField] private CreateSlider slider;
    private Color currentColor;
	private float nextSliderPositon;
    
	[Space(20)]
	[SerializeField] private bool isShakerEmptied;

    private void Awake()
    {
        typeOfDrinkInside = new Dictionary<TypeOfDrinks.TypeOfDrink, int>();
        currentLayer = gameObject.layer;
	    drinkState = TypeOfCocktails.StateOfCocktail.Idle;
        currentColor = Color.clear;
        nextSliderPositon = 0;
    }
    private void Update()
    {
        if (isShaker)
        {
	        FillShaker();
	        isShakerEmptied = false;
        }
        else
        {
            FillDrink();
        }
        
	    if (isShakerEmptied)
	    {
	    	ResetDrink();
	    	isShakerEmptied = false;
	    	Debug.Log("Shaker Reseted");
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
	    DestroyChildrens(slider.transform);
	    numberOfParticles = 0;
	    currentSlider = null;
	    nextSliderPositon = 0;
	    currentColor = Color.magenta;
	    drinkState = TypeOfCocktails.StateOfCocktail.Idle;
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
	
	// Temporal, use later
	public Dictionary<TypeOfDrinks.TypeOfDrink, int> TraspassDrinks()
	{
		Dictionary<TypeOfDrinks.TypeOfDrink, int> newDictionary = new Dictionary<TypeOfDrinks.TypeOfDrink, int>(typeOfDrinkInside);
		
		foreach (TypeOfDrinks.TypeOfDrink drinkType in typeOfDrinkInside.Keys)
		{
			if (typeOfDrinkInside[drinkType] > 0)
			{
				newDictionary[drinkType] = typeOfDrinkInside[drinkType] - 1;
			}
		}
		
		typeOfDrinkInside = newDictionary;
		return typeOfDrinkInside;
	}
	
	
    public Dictionary<TypeOfDrinks.TypeOfDrink, int> GetTypeOfDrinkInside()
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
	    if (numberOfParticles <= 2)
	    {
	    	isShakerEmptied = true;
	    }
    }
	public void SetDrinkState(TypeOfCocktails.StateOfCocktail _drinkState)
    {
        drinkState = _drinkState;
    }
	private void DestroyChildrens(Transform root)
	{
		foreach (Transform item in root)
		{
			Destroy(item.gameObject);
		}
	}
	
	public void CreateCocktail()
	{
		if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.GlacierSpirit) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Moszkowski) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.LemonJuice)
		)
		{
			int glacierSpirit = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.GlacierSpirit];
			int moszkowski = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Moszkowski];
			int lemonJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.LemonJuice];

			if (glacierSpirit >= 15 && moszkowski >= 15 && lemonJuice >= 55 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.PinkLeibel;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.HerbHaven) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.GlacierSpirit) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Tonic)
		)
		{
			int herbHaven = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.HerbHaven];
			int glacierSpirit = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.GlacierSpirit];
			int tonic = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Tonic];

			if (herbHaven >= 55 && glacierSpirit >= 5 && tonic >= 25 && drinkState == TypeOfCocktails.StateOfCocktail.Idle)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.DiscoN;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.HerbHaven) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Soda) 
		)
		{
			int herbHaven = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.HerbHaven];
			int soda = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Soda];

			if (herbHaven >= 45 && soda >= 45 && drinkState == TypeOfCocktails.StateOfCocktail.Mixed)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Invade;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.HerbHaven) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.OrangeJuice) 
		)
		{
			int herbHaven = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.HerbHaven];
			int orangeJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.OrangeJuice];
			
			if (herbHaven >= 55 && orangeJuice >= 35 && drinkState == TypeOfCocktails.StateOfCocktail.Mixed)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Thresh;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.DesertRose) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.GlacierSpirit) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.LemonJuice) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Soda)
		)
		{
			int desertRose = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.DesertRose];
			int glacierSpirit = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.GlacierSpirit];
			int lemonJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.LemonJuice];
			int soda = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Soda];

			if (desertRose >= 15 && glacierSpirit >= 25 && lemonJuice >= 15 && soda >= 25 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.LobsterCrami;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.GlacierSpirit) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.OrangeJuice) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Soda) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Moszkowski)
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int glacierSpirit = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.GlacierSpirit];
			int orangeJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.OrangeJuice];
			int soda = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Soda];
			int moszkowski = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Moszkowski];

			if (rusticGold >= 5 && glacierSpirit >= 15 && orangeJuice >= 25 && soda >= 5 && moszkowski >= 15 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Razz;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.DesertRose) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Tonic)
		)
		{
			int desertRose = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.DesertRose];
			int tonic = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Tonic];

			if (desertRose >= 35 && tonic >= 55 && drinkState == TypeOfCocktails.StateOfCocktail.Mixed)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Morgana;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.LemonJuice) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Soda)
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int lemonJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.LemonJuice];
			int soda = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Soda];
			
			if (rusticGold >= 55 && lemonJuice >= 15 && soda >= 15 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.MoszkowskiFlip;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.DesertRose) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Moszkowski) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Cola) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Tonic)
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int desertRose = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.DesertRose];
			int moszkowski = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Moszkowski];
			int cola = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Cola];
			int tonic = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Tonic];
			
			if (rusticGold >= 15 && desertRose >= 15 && moszkowski >= 15 && cola >= 15 && tonic >= 15 && drinkState == TypeOfCocktails.StateOfCocktail.Idle)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.PipiStrate;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.DesertRose) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Moszkowski) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Cola) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Tonic)
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int desertRose = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.DesertRose];
			int moszkowski = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Moszkowski];
			int cola = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Cola];
			int tonic = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Tonic];
			
			if (rusticGold >= 15 && desertRose >= 15 && moszkowski >= 15 && cola >= 15 && tonic >= 15 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.PipiStrate;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.DesertRose) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.LemonJuice) 
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int desertRose = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.DesertRose];
			int lemonJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.LemonJuice];
			
			if (rusticGold >= 35 && desertRose >= 25 && lemonJuice >= 25 && drinkState == TypeOfCocktails.StateOfCocktail.Idle)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Sekiro;
			}
		}
		else if (
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.RusticGold) && 
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.Tonic) &&
			typeOfDrinkInside.ContainsKey(TypeOfDrinks.TypeOfDrink.LemonJuice) 
		)
		{
			int rusticGold = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.RusticGold];
			int tonic = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.Tonic];
			int lemonJuice = typeOfDrinkInside[TypeOfDrinks.TypeOfDrink.LemonJuice];
			
			if (rusticGold >= 45 && tonic >= 25 && lemonJuice >= 15 && drinkState == TypeOfCocktails.StateOfCocktail.Shaked)
			{
				typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Tiefti;
			}
		}
		else 
		{
			typeOfCocktail = TypeOfCocktails.TypeOfCocktail.Mierdon;
		}
	}
}
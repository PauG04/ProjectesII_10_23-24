using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ResultManager : MonoBehaviour
{
	[Header("Renderer Variables")]
	[SerializeField] private Renderer fluidRenderer;
	[SerializeField] private int maxCapacity;
	
	private float fill;
	private int currentLayer;
	private int numberOfParticles = 0;
	
	[Header("Liquid Fill Variables")]
	[SerializeField] private float maxColliderPos = 0.1475f;
	[SerializeField] private float minColliderPos = -0.23f;
	
	[Header("Text Variables")]
	[SerializeField] private TextMeshPro textMeshPro;
	
	private Dictionary<LiquidManager.TypeOfCocktail, int> cocktail;

	protected void Awake()
	{
		cocktail = new Dictionary<LiquidManager.TypeOfCocktail, int>();
		currentLayer = gameObject.layer;
	}
	protected void Update()
	{
		FillDrink();
		
		if (numberOfParticles >= maxCapacity)
		{
			textMeshPro.text = GetMostPopularCocktail().ToString();
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Liquid") && numberOfParticles < maxCapacity)
		{
			LiquidParticle particleCollision = collision.GetComponent<LiquidParticle>();

			if (!cocktail.ContainsKey(particleCollision.cocktailType))
			{
				cocktail.Add(particleCollision.cocktailType, 1);
			}
			else
			{
				cocktail[particleCollision.cocktailType]++;
			}
			Debug.Log(particleCollision.cocktailType + " has " + cocktail[particleCollision.cocktailType] + " particles inside.");

			Destroy(collision.gameObject);
			numberOfParticles++;
		}
	}
	private LiquidManager.TypeOfCocktail GetMostPopularCocktail()
	{
		if (cocktail.Values.Count() == 0)
		{
			return LiquidManager.TypeOfCocktail.Mierdon;
		}
		
		var mostPopularCocktail = cocktail.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
		
		return mostPopularCocktail;
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
}

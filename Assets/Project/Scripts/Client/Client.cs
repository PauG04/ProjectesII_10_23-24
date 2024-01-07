using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class Client : MonoBehaviour
{
	[SerializeField] private PlayerConversant playerConversant;
	
	[SerializeField] private List<TypeOfCocktails.TypeOfCocktail> typeOfDrinkList;
	[SerializeField] private TypeOfCocktails.TypeOfCocktail drinkThatWants;
	private SpriteRenderer spriteRenderer;

	[SerializeField] public TypeOfCocktails.TypeOfCocktail drinkDropped;

	[SerializeField] private GameObject badDrink;
	[SerializeField] private GameObject goodDrink;

	private bool blink;
	[SerializeField] private Sprite idleSprite;
	[SerializeField] private Sprite blinkSprite;

	[SerializeField] private Sprite badDrinkSprite;
	[SerializeField] private Sprite goodDrinkSprite;

	[SerializeField] private bool playOnce;

	private StateManager stateManager;

	private bool playHappySound = true;
	private bool playMadSound = true;

	private void Awake()
	{
		playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
		
		spriteRenderer = GetComponent<SpriteRenderer>();

		badDrink.SetActive(false);
		goodDrink.SetActive(false);

		badDrink.transform.localScale = Vector3.zero;
		goodDrink.transform.localScale = Vector3.zero;

		playOnce = true;
		blink = true;
	}

    void Update()
    {
	    CheckDrink();
    }
    
	public void ChoseDrink()
	{
		Debug.Log(playerConversant.GetChildNumber());
		
		switch (playerConversant.GetChildNumber())
		{
		case 0:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.LobsterCrami;
			break;
		case 1:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.PinkLeibel;
			break;
		case 2:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.DiscoN;
			break;
		case 3:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Invade;
			break;
		case 4:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Morgana;
			break;
		case 5:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.MoszkowskiFlip;
			break;
		case 6:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.PipiStrate;
			break;
		case 7:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Razz;
			break;
		case 8:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Sekiro;
			break;
		case 9:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Thresh;
			break;
		case 10:
			drinkThatWants = TypeOfCocktails.TypeOfCocktail.Tiefti;
			break;
		default:
			break;
		}
	}
    
	private void CheckDrink()
	{
		if (drinkDropped == TypeOfCocktails.TypeOfCocktail.Empty)
		{
			if (blink)
			{
				StartCoroutine(Blink());
				blink = false;
			}
			playOnce = true;
		}
		else if (drinkDropped == drinkThatWants)
		{
			GoodDrinkServed();
		}
		else if(drinkDropped != drinkThatWants)
		{
			BadDrinkServed();
		}
	}

	private void GoodDrinkServed()
	{
		if (playOnce)
		{
			if (playHappySound)
			{
				AudioManager.instance.Play("happyClient");
				playHappySound = false;
			}
			spriteRenderer.sprite = goodDrinkSprite;

			goodDrink.SetActive(true);

			goodDrink.transform.SetParent(null);
			goodDrink.GetComponent<SpriteRenderer>().sortingOrder = 3;

			goodDrink.transform.position = new Vector3(
				goodDrink.transform.position.x,
				goodDrink.transform.position.y,
				10
			);
			goodDrink.transform.position = Vector3.Lerp(goodDrink.transform.position, new Vector3(goodDrink.transform.position.x, goodDrink.transform.position.y + 0.1f, goodDrink.transform.position.z), 3 * Time.deltaTime);
			goodDrink.transform.localScale = Vector3.Lerp(goodDrink.transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

			if (goodDrink.transform.localScale == Vector3.one)
			{
				spriteRenderer.sprite = idleSprite;
				goodDrink.transform.SetParent(transform.parent);
				goodDrink.SetActive(false);
				playOnce = false;	
				
				AddPositiveStates();
					
				drinkDropped = TypeOfCocktails.TypeOfCocktail.Empty;
				goodDrink.transform.localScale = Vector3.zero;
				goodDrink.transform.localPosition = Vector3.zero;
				playHappySound = true;
			}
		}
	}

	private void AddPositiveStates()
	{
		MoneyManager.instance.AddDayEarnings(10);
		StateManager.instance.AddToState("Fatigue", 1);
		StateManager.instance.AddToState("Stress", -1);
	}

	private void BadDrinkServed()
	{
		if (playOnce)
		{
			if (playMadSound)
			{
				AudioManager.instance.Play("madClient");
				playMadSound = false;
			}
			spriteRenderer.sprite = badDrinkSprite;

			badDrink.SetActive(true);

			badDrink.transform.SetParent(null);
			badDrink.GetComponent<SpriteRenderer>().sortingOrder = 3;

			badDrink.transform.position = new Vector3(
				badDrink.transform.position.x,
				badDrink.transform.position.y,
				10
			);
			badDrink.transform.position = Vector3.Lerp(badDrink.transform.position, new Vector3(badDrink.transform.position.x, badDrink.transform.position.y + 0.1f, badDrink.transform.position.z), 3 * Time.deltaTime);
			badDrink.transform.localScale = Vector3.Lerp(badDrink.transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

			if (badDrink.transform.localScale == Vector3.one)
			{
				spriteRenderer.sprite = idleSprite;
				badDrink.transform.SetParent(transform.parent);
				badDrink.SetActive(false);
				playOnce = false;
					
				AddNegativeStates();
					
				drinkDropped = TypeOfCocktails.TypeOfCocktail.Empty;
				badDrink.transform.localScale = Vector3.zero;
				badDrink.transform.localPosition = Vector3.zero;
				playMadSound = true;
				
			}
		}
	}
	private void AddNegativeStates()
	{
		StateManager.instance.AddToState("Health", -1);
		StateManager.instance.AddToState("Fatigue", 2);
		StateManager.instance.AddToState("Stress", 2);
	}
	IEnumerator Blink()
	{
		yield return new WaitForSeconds(Random.Range(1, 5));
		spriteRenderer.sprite = blinkSprite;
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.sprite = idleSprite;
		blink = true;
	}
}

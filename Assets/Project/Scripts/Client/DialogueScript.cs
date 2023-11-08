using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private List<TypeOfCocktail> typeOfDrinkList;
    [SerializeField] private TypeOfCocktail drinkThatWants;
    [SerializeField] public TypeOfCocktail drinkDropped;

    [SerializeField] private GameObject badDrink;
    [SerializeField] private GameObject goodDrink;
    [SerializeField] private bool playOnce;

    private void Awake()
    {
        badDrink.SetActive(false);
        goodDrink.SetActive(false);

        badDrink.transform.localScale = Vector3.zero;
        goodDrink.transform.localScale = Vector3.zero;

        playOnce = true;

        RandomDrinkChose();
    }

    private void Update()
    {
        CheckDrink();
    }

    private void RandomDrinkChose()
    {
        drinkThatWants = typeOfDrinkList[Random.Range(0, typeOfDrinkList.Count - 1)];
        textMeshPro.text = "I want a " + drinkThatWants.ToString();
    }
    private void CheckDrink()
    {
        if (drinkDropped == TypeOfCocktail.Empty)
        {
            textMeshPro.text = "I want a " + drinkThatWants.ToString();
            playOnce = true;
        }
        else if (drinkDropped == drinkThatWants)
        {
            textMeshPro.text = "That drink was delicious!";
            AudioManager.instance.Play("happyClient");

            if (playOnce)
            {
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
                    goodDrink.transform.SetParent(transform.parent);
                    goodDrink.SetActive(false);
                    playOnce = false;
                    RandomDrinkChose();
                    drinkDropped = TypeOfCocktail.Empty;
                    goodDrink.transform.localScale = Vector3.zero;
                    goodDrink.transform.localPosition = Vector3.zero;
                }
            }
        }
        else if(drinkDropped != drinkThatWants)
        {
            textMeshPro.text = "What a piece of shit";
            AudioManager.instance.Play("madClient");

            if (playOnce)
            {
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
                    badDrink.transform.SetParent(transform.parent);
                    badDrink.SetActive(false);
                    playOnce = false;
                    RandomDrinkChose();
                    drinkDropped = TypeOfCocktail.Empty;
                    badDrink.transform.localScale = Vector3.zero;
                    badDrink.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}

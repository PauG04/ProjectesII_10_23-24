using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WikiManager : MonoBehaviour
{
    public static WikiManager instance { get; private set; }

    [SerializeField] private List<Cocktail> cocktails;

    [SerializeField] private WikiPage firstPage;
    [SerializeField] private WikiPage secondPage;

    private int pageNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        pageNumber = 0;
        UpdatePages(pageNumber);
    }

    public void NextPage()
    {
        if (pageNumber + 2 <= cocktails.Count)
        {
            pageNumber += 2;
            UpdatePages(pageNumber);
        }
    }

    public void PrevPage()
    {
        if (pageNumber >= 2)
        {
            pageNumber -= 2;
            UpdatePages(pageNumber);
        }
    }

    public void AddPage(Cocktail cocktail)
    {
        cocktails.Add(cocktail);
    }

    private void UpdatePages(int page)
    {
        firstPage.UpdatePage(cocktails[page]);

        if (page + 1 < cocktails.Count)
            secondPage.UpdatePage(cocktails[page + 1]);
        else
            secondPage.ClearPage();
    }

    public List<Cocktail> GetAvailableCocktails()
    { 
        return cocktails; 
    }
}

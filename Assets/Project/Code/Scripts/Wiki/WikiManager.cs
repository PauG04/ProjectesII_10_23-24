using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WikiManager : MonoBehaviour
{
    public static WikiManager instance { get; private set; }

    [SerializeField] private List<CocktailNode> cocktails;

    [SerializeField] private GameObject wiki;
    [SerializeField] private WikiPage firstPage;
    [SerializeField] private WikiPage secondPage;

    [SerializeField] private DragItems book;
    private bool bookIsOpened;

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

        bookIsOpened = false;
        pageNumber = 0;
    }

    private void Update()
    {
        if (book.GetInsideWorkspace() && !bookIsOpened)
        {
            bookIsOpened = true;
            OpenWiki();
        }
        else if (!book.GetInsideWorkspace() && bookIsOpened)
        {
            bookIsOpened = false;
            CloseWiki();
        }
    }

    public void NextPage()
    {
        if (pageNumber + 2 <= cocktails.Count)
        {
            AudioManager.instance.Play("TurnPage", "SFX");
            pageNumber += 2;
            UpdatePages(pageNumber);
        }
    }

    public void PrevPage()
    {
        if (pageNumber >= 2)
        {
            AudioManager.instance.Play("TurnPage", "SFX");
            pageNumber -= 2;
            UpdatePages(pageNumber);
        }
    }

    public void AddPage(CocktailNode cocktail)
    {
        cocktails.Add(cocktail);
    }

    private void UpdatePages(int page)
    {
        if (page < cocktails.Count)
        {
            firstPage.UpdatePage(cocktails[page]);

            if (page + 1 < cocktails.Count)
                secondPage.UpdatePage(cocktails[page + 1]);
            else
                secondPage.ClearPage();
        }
    }

    public List<CocktailNode> GetAvailableCocktails()
    { 
        return cocktails; 
    }

    private void OpenWiki()
    {
        AudioManager.instance.Play("OpenWiki", "SFX");
        firstPage.gameObject.SetActive(true);
        secondPage.gameObject.SetActive(true);
        UpdatePages(pageNumber);
    }

    private void CloseWiki()
    {
        firstPage.gameObject.SetActive(false);
        secondPage.gameObject.SetActive(false);
    }
}

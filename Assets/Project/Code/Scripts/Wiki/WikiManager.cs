using System.Collections.Generic;
using UnityEngine;

public class WikiManager : MonoBehaviour
{
    public static WikiManager instance { get; private set; }

    [SerializeField] private List<GameObject> leftPages;
    [SerializeField] private List<GameObject> rightPages;

    [SerializeField] private GameObject wikiPages;
    private GameObject leftPage;
    private GameObject rightPage;

    [SerializeField] private Sprite prevAndNextSprite;
    [SerializeField] private Sprite prevSprite;
    [SerializeField] private Sprite nextSprite;

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

        leftPage = null;
        rightPage = null;
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

    public void PrevPage()
    {
        if (pageNumber > 0)
        {
            AudioManager.instance.PlaySFX("TurnPage");
            pageNumber -= 1;
            UpdatePages(pageNumber);
        }
    }

    public void NextPage()
    {
        if (pageNumber < rightPages.Count - 1)
        {
            AudioManager.instance.PlaySFX("TurnPage");
            pageNumber += 1;
            UpdatePages(pageNumber);
        }
    }

    private void UpdatePages(int page)
    {
        for (int i = wikiPages.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(wikiPages.transform.GetChild(i).gameObject);
        }

        leftPage = Instantiate(leftPages[page], wikiPages.transform);
        if (leftPage.TryGetComponent<CocktailPage>(out CocktailPage leftCocktail))
            leftCocktail.InitPage();

        rightPage = Instantiate(rightPages[page], wikiPages.transform);
        if (rightPage.TryGetComponent<CocktailPage>(out CocktailPage rightCocktail))
            rightCocktail.InitPage();

        if (page == 0)
            book.SetWorkspaceSprite(nextSprite);
        else if (page == leftPages.Count - 1)
            book.SetWorkspaceSprite(prevSprite);
        else
            book.SetWorkspaceSprite(prevAndNextSprite);

        return;
    }

    private void OpenWiki()
    {
        AudioManager.instance.PlaySFX("OpenWiki");
        if (leftPage != null)
            leftPage.SetActive(true);
        if (rightPage != null)
            rightPage.SetActive(true);
        UpdatePages(pageNumber);
    }

    private void CloseWiki()
    {
        AudioManager.instance.PlaySFX("CloseWiki");
        leftPage.SetActive(false);
        rightPage.SetActive(false);
    }

    public int GetCurrentPage()
    {
        return pageNumber;
    }
}

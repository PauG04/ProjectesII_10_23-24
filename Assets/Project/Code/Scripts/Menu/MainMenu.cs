using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private LevelLoader levelLoader;

    [Header("Screens")]
    [SerializeField] private GameObject optionsScreen;
    private GameObject currentApp;

    [Header("Buttons")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnButton;

    [Header("Animation Values")]
    [SerializeField] private float animationDuration;
    private void Start()
    {
        optionsScreen.SetActive(false);

        optionsButton.onClick.AddListener(() => {
            OpenApplication(optionsScreen);
            });

        returnButton.onClick.AddListener(() => CloseApplication());
    }
    public void PlayButton()
    {
        levelLoader.LoadNextLevel();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OpenApplication(GameObject appToOpen)
    {
        currentApp = appToOpen;
        appToOpen.SetActive(true);
        StartCoroutine(OpenAnimation(appToOpen.transform));
    }

    public void CloseApplication()
    {
        if (currentApp != null)
        {
            mainMenu.SetActive(true);
            StartCoroutine(CloseAnimation(currentApp.transform));
            currentApp = null;
        }
    }

    IEnumerator OpenAnimation(Transform app)
    {
        float time = 0.0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            app.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            mainMenu.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return null;
            time += Time.deltaTime;
        }

        mainMenu.SetActive(false);
        app.localScale = Vector3.one;
    }

    IEnumerator CloseAnimation(Transform app)
    {
        float time = 0.0f;
        Vector3 startScale = app.localScale;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            app.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            mainMenu.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return null;
            time += Time.deltaTime;
        }

        app.gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenApp : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;

    [Header("Applications Screens")]
    [SerializeField] private GameObject optionsApp;
    [SerializeField] private GameObject shopApp;
    [SerializeField] private GameObject musicApp;
    [SerializeField] private GameObject slotApp;

    private GameObject currentApp;

    [Header("Applications Buttons")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button slotButton;

    [Space(20)]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button homeButton;

    [Header("Scrollable Apps")]
    [SerializeField] private Scrollbar[] scollBars;

    [Header("Animation Values")]
    [SerializeField] private float animationDuration;

    private void Start()
    {
        optionsButton.onClick.AddListener(() => OpenApplication(optionsApp));
        shopButton.onClick.AddListener(() => OpenApplication(shopApp));
        musicButton.onClick.AddListener(() => OpenApplication(musicApp));
        slotButton.onClick.AddListener(() => OpenApplication(slotApp));

        returnButton.onClick.AddListener(() => CloseApplication());
        homeButton.onClick.AddListener(() => CloseApplication());
    }

    public void OpenApplication(GameObject appToOpen)
    {
        AudioManager.instance.PlaySFX("OpenApp");
        currentApp = appToOpen;
        appToOpen.SetActive(true);
        StartCoroutine(OpenAnimation(appToOpen.transform));

    }

    public void CloseApplication()
    {
        AudioManager.instance.PlaySFX("CloseApp");
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
            yield return null;
            time += Time.deltaTime;

            foreach (Scrollbar bar in scollBars)
            {
                bar.value = 1;
            }
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
            yield return null;
            time += Time.deltaTime;
        }

        app.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}

using System;
using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TypeWriterEffect : MonoBehaviour
{
    private TMP_Text textBox;

    private int currentVisibleCharacterIndex;
    private Coroutine typeWriterCoroutine;

    private WaitForSeconds simpleDelay;
    private WaitForSeconds interpunctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float characterPerSecond = 20;
    [SerializeField] private float interpunctuationDelayInput = 0.5f;

    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds skipDelay;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip;
    [SerializeField] [Min(1)] private int skipSpeedup = 5;

    private WaitForSeconds textboxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static bool isTextCompleted = false;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;

    [Header("Visual Components")]
    [SerializeField] private GameObject nextButton;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();

        simpleDelay = new WaitForSeconds(1 / characterPerSecond);
        interpunctuationDelay = new WaitForSeconds(interpunctuationDelayInput);

        skipDelay = new WaitForSeconds(1 / (characterPerSecond * skipSpeedup));
        textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textBox.textInfo.characterCount != textBox.maxVisibleCharacters - 1)
            {
                Skip();
            }
        }
        if (isTextCompleted)
        {
            nextButton.SetActive(true);
        }
    }

    public void SetText(string text)
    {

        if (typeWriterCoroutine != null)
        {
            StopCoroutine(typeWriterCoroutine);
        }

        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        isTextCompleted = false;
        typeWriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            int lastCharacterIndex = textInfo.characterCount - 1;
            
            if (currentVisibleCharacterIndex >= lastCharacterIndex && lastCharacterIndex > 0)
            {
                textBox.maxVisibleCharacters++;
                yield return textboxFullEventDelay;
                isTextCompleted = true;
                CompleteTextRevealed?.Invoke();
                yield break;
            }

            char character = textInfo.characterInfo[currentVisibleCharacterIndex].character;

            textBox.maxVisibleCharacters++;

            if (!CurrentlySkipping &&
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                character == ';' || character == '!' || character == '-'))
            {
                yield return interpunctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? skipDelay : simpleDelay;
            }

            CharacterRevealed?.Invoke(character);
            currentVisibleCharacterIndex++;
        }
    }

    private void Skip()
    {
        if (CurrentlySkipping)
        {
            return;
        }

        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        StopCoroutine(typeWriterCoroutine);
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
        //CompleteTextRevealed?.Invoke();
        isTextCompleted = true;
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }
}
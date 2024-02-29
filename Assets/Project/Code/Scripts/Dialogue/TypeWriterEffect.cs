using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

[RequireComponent(typeof(TMP_Text))]
public class TypeWriterEffect : MonoBehaviour
{
    [Header("Test String")]
    [SerializeField] private string text;

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

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();

        simpleDelay = new WaitForSeconds(1 / characterPerSecond);
        interpunctuationDelay = new WaitForSeconds(interpunctuationDelayInput);

        skipDelay = new WaitForSeconds(1 / (characterPerSecond * skipSpeedup));
        textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void Start()
    {
        SetText(text);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (textBox.maxVisibleCharacters != textBox.textInfo.characterCount)
            {
                Skip();
            }
        }
    }

    public void SetText(string text)
    {
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        typeWriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            if (currentVisibleCharacterIndex == lastCharacterIndex)
            {
                textBox.maxVisibleCharacters++;
                yield return textboxFullEventDelay;
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
        CompleteTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }
}

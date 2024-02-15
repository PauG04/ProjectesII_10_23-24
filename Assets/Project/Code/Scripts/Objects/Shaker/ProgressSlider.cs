using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSlider : MonoBehaviour
{
    [Header("Shaker")]
    [SerializeField] private ShakerStateMachine shaker;

    [Header("Slider childs")]
    [SerializeField] private GameObject[] slider;
    [SerializeField] private float r;
    [SerializeField] private float g;
    [SerializeField] private float b;

    [Header("Velocity Color Lerp")]
    [SerializeField] private float velocityColor;

    private SpriteRenderer[] sliderSprite;

    private float maxProgress;
    private float value;
    private int currentSlider;

    private bool startLerp;
    private SpriteRenderer _color;
    
    private void Start()
    {
        maxProgress = shaker.GetMaxProgress();
        value = maxProgress / slider.Length;
        currentSlider = 0;

        sliderSprite = new SpriteRenderer[slider.Length];
        for (int i = 0; i < sliderSprite.Length; i++)
        {
            slider[i].SetActive(false);
            sliderSprite[i] = slider[i].GetComponent<SpriteRenderer>();
            sliderSprite[i].color = new Color(255, 255, 255, 0);
        }

        _color = GetComponent<SpriteRenderer>();
        _color.color = new Color(_color.color.r, _color.color.g, _color.color.b, 0);

        startLerp = false;
    }

    private void Update()
    {
        ActiveSlider();
        AlphaLerp();
    }

    private void ActiveSlider()
    {
        if(shaker.GetProgress() > value)
        {
            slider[currentSlider].SetActive(true);
            sliderSprite[currentSlider].color = new Color(r, g, b);
            currentSlider++;
            g -= 0.1f;
            value += maxProgress / 10;
        }
    }

    private void AlphaLerp()
    {
        if (startLerp)
        {
            Color newColor = _color.color;
            newColor.a = Mathf.Lerp(_color.color.a, 1, Time.deltaTime * velocityColor);

           _color.color = newColor;

            for (int i = 0; i < sliderSprite.Length; i++)
            {
                Color newSpriteColor = sliderSprite[i].color;
                newSpriteColor.a = Mathf.Lerp(sliderSprite[i].color.a, 1, Time.deltaTime * velocityColor);

                sliderSprite[i].color = newSpriteColor;
            }
        }
        if (!startLerp)
        {
            Color newColor = _color.color;
            newColor.a = Mathf.Lerp(_color.color.a, 0, Time.deltaTime * velocityColor);

            _color.color = newColor;

            for (int i = 0; i < sliderSprite.Length; i++)
            {
                Color newSpriteColor = sliderSprite[i].color;
                newSpriteColor.a = Mathf.Lerp(sliderSprite[i].color.a, 0, Time.deltaTime * velocityColor);

                sliderSprite[i].color = newSpriteColor;
            }
        }
    }

    public void ResetSlider()
	{
		shaker.SetProgress(0);
        value = maxProgress / slider.Length;
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].SetActive(false);
            sliderSprite[i].color = new Color(255, 255, 255);
        }
        currentSlider = 0;
        g = 1;
    }

    public void SetIsLerp(bool state)
    {
        startLerp = state;
    }
}

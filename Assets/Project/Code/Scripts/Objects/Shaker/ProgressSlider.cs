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

    private SpriteRenderer[] sliderSprite;

    private float maxProgress;
    private float value;
    private int currentSlider;
    
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
            sliderSprite[i].color = new Color(255, 255, 255, 255);
        }
    }

    private void Update()
    {
        ActiveSlider();
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

    public void ResetSlider()
	{
		shaker.SetProgress(0);
        value = maxProgress / slider.Length;
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].SetActive(false);
            sliderSprite[i].color = new Color(255, 250, 250, 255);
        }
        currentSlider = 0;
        g = 1;
    }
}

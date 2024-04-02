using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Votation : MonoBehaviour
{
    [SerializeField] private Slider ginTonic;
    [SerializeField] private Slider ronCola;
    [SerializeField] private StreamerEvent streamerEvent;
    [SerializeField] private Color colorSlider;

    private float time = 0;
    private float maxTime = 0.1f;

    private float currentVotes;
    private float currentGinVotes;
    private float currentRonVotes;

    private void Awake()
    {
        ginTonic.value = 0;
        ronCola.value = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(streamerEvent.GetVotation() && time > maxTime)
        {
            currentVotes++;

            int randomValue = Random.Range(0, 10);
            if (randomValue >= 5 || currentRonVotes >= currentGinVotes)
            {
                currentGinVotes++;
                SetSliderValue(ginTonic, currentGinVotes);
            }
            else if (randomValue < 5)
            {
                currentRonVotes++;
                SetSliderValue(ronCola, currentRonVotes);
            }
            time = 0;
        }
        if(!streamerEvent.GetVotation())
        {
            ginTonic.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = colorSlider;
        }
    }

    private void SetSliderValue(Slider slider, float votes)
    {
        slider.value = (votes / currentVotes);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameObject endDayWindow;

    private float seconds;
    private int minutes;
    private int hours;

    private bool timerStopped;

    private TextMeshProUGUI timerText;

    private void Awake()
    {
        seconds = 0.0f;
        minutes = 0;
        hours = 0;

        timerStopped = true;

        timerText = GetComponentInChildren<TextMeshProUGUI>();

        StartDay();
    }

    private void Update()
    {
        if (!timerStopped)
        {
            UpdateTime();
            UpdateText();
        }

    }

    public void StartDay()
    {
        timerStopped = false;
        hours = 10;
        minutes = 0;
        seconds = 0.0f;
    }

    public void EndDay()
    {
        timerStopped = true;
        hours = 2;
        minutes = 0;
        seconds = 0.0f;

        Instantiate(endDayWindow, transform);
        // Parar Timer
        // Crear/Abrir una ventana de Final del dia
        // La ventana tiene que tener: 
        // Boton de siguiente dia
        // Dinero Actual
        // Impuestos Diarios
        // Ganancias del día
    }

    private void UpdateTime()
    {
        seconds += Time.deltaTime * 84;

        if (seconds >= 60.0f)
        {
            seconds = 0;
            minutes++;
            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
                if (hours >= 24)
                {
                    hours = 0;
                }
                else if (hours == 2)
                {
                    EndDay();
                }
            }
        }
    }
    private void UpdateText()
    {
        timerText.text = hours.ToString("00") + ":" + minutes.ToString("00");
    }

}

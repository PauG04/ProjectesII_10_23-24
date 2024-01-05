using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float seconds;
    private int minutes;
    private int hours;

    private bool timerStopped;

    private TextMeshProUGUI timeText;

    private void Awake()
    {
        seconds = 0.0f;
        minutes = 0;
        hours = 0;

        timerStopped = false;

        timeText = GetComponent<TextMeshProUGUI>();
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
    }

    public void EndDay()
    {
        timerStopped = true;



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
        seconds += Time.deltaTime;

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
        timeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

}

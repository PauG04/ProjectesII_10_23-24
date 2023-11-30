using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class StartCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] mesage;
    [SerializeField] private float time;

    private Vector3 shakeScale;
    private bool showMesage;
    private bool startShowMesage;
    private bool startMinigame;
    int id;


    private void Start()
    {
        id = 0;
        showMesage = false;
        startMinigame = false;
        shakeScale = mesage[0].transform.localScale;
        for(int i = 0; i< mesage.Length; i++)
        {
            mesage[i].transform.localScale = new Vector3(0, 0, 0);
        }      
    }

    private void Update()
    {
        if(startShowMesage)
        {
            StartTimer();
        }
    }

    private void OnMouseDown()
    {
        if(!startMinigame && !startShowMesage)
        {
            startShowMesage = true;
            showMesage = true;
        }
    }

    private void StartTimer()
    {
         if(showMesage)
         {
             mesage[id].transform.localScale = Vector3.Lerp(mesage[id].transform.localScale, shakeScale, time * Time.deltaTime);
             isBigger();
         }
         else
         {
             mesage[id].transform.localScale = Vector3.Lerp(mesage[id].transform.localScale, new Vector3(0, 0, 0), time * Time.deltaTime);
             isLower();
         }              
        
    }

    private void isBigger()
    {
        if (mesage[id].transform.localScale.y >= shakeScale.y - 0.02)
        {
            showMesage = false;
        }
    }

    private void isLower()
    {
        if (mesage[id].transform.localScale.y <= 0.001)
        {
            showMesage = true;
            id++;
        }
        if(id == mesage.Length)
        {
            startShowMesage = false;
            startMinigame = true;
            id = 0;
        }      
    }

    public bool GetStartMinigae()
    {
        return startMinigame;
    }

    public void SetStartMinigae(bool state)
    {
        startMinigame = state;
    }
}

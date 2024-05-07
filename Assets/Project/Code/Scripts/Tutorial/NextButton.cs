using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    private int currentDialogue;
    private float timer;
    private float maxTime;
    private bool isActtive;

    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private GameObject text;

    private void Start()
    {
        currentDialogue = 0;
        timer = 0;
        maxTime = 1;
        isActtive = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Desactive();
    }

    private void OnMouseDown()
    {
        if(timer > maxTime)
        {
            currentDialogue++;
            timer = 0;
            if (playerConversant.HasNext())
            {
                //playerConversant.Next();
            }
        }
    }
    private void Desactive()
    {
        if (currentDialogue == 4 || currentDialogue == 6)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            text.SetActive(false);
            isActtive = false;
        }
    }

    public void Active()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        text.SetActive(true);
        currentDialogue++;
        isActtive = true;
    }

    public bool GetIsActive()
    {
        return isActtive;
    }

    
}

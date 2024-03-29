using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    private int currentDialogue;
    private float timer;
    private float maxTime;

    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private GameObject text;

    private void Start()
    {
        currentDialogue = 0;
        timer = 0;
        maxTime = 1;

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
                playerConversant.Next();
            }
        }
    }
    private void Desactive()
    {
        if (currentDialogue == 7 || currentDialogue == 9 || currentDialogue == 10 || currentDialogue == 11 || currentDialogue == 12 || currentDialogue == 13 || currentDialogue == 14)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            text.SetActive(false);
        }
    }

    public void Active()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        text.SetActive(true);
        currentDialogue++;
    }

    
}

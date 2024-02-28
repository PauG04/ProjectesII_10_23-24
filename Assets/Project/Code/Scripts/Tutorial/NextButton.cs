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
        if (currentDialogue == 3 || currentDialogue == 5 || currentDialogue == 8 || currentDialogue == 10 || currentDialogue == 12)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void Active()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        currentDialogue++;
    }

    
}

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

    public void Active()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        currentDialogue++;
    }

    public void Desactive()
    {
        if (currentDialogue == 2 || currentDialogue == 4 || currentDialogue == 7 || currentDialogue == 9 || currentDialogue == 11)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

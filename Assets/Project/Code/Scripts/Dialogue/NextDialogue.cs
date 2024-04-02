using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.UIElements;

public class NextDialogue : MonoBehaviour
{
	[SerializeField] private PlayerConversant playerConversant;

    private void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
    }

    private void OnMouseDown()
    {
        if (TypeWriterEffect.isTextCompleted && playerConversant.HasNext())
        {
            playerConversant.Next();
        }
        else
        {
            // TypeWritterEffect Skip
        }
    }
}

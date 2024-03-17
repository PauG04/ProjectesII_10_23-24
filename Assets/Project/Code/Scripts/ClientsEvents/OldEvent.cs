using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEvent : MonoBehaviour
{
    [SerializeField] private Dialogue.Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Client") && gameObject.GetComponent<DragItems>().GetWasOnTheTable())
        {
            collision.GetComponent<AIConversant>().SetDialogue(dialogue);
            collision.GetComponent<AIConversant>().HandleDialogue();
            Destroy(gameObject);
            collision.GetComponent<Client>().SetTimer(true);
        }
    }
}

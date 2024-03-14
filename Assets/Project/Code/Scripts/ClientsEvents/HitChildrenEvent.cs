using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChildrenEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    private bool lastDialogue = false;

    private ClientNode client;
    private GameObject clientObject;


    private void Update()
    {
        if (client != null && client == eventClient)
        { 
            if(clientObject.GetComponent<BoxCollider2D>().enabled)
            {
                SetDialogue();
            }          
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(lastDialogue)
        {
            if(!playerConversant.HasNext())
            {
                clientObject.GetComponent<Client>().SetTimer(true);
                enabled = false;
            }
        }

    }

    public void SetDialogue()
    {
        switch (clientObject.GetComponent<Client>().GetCurrentsHit())
        {
            case 0:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                break;
            case 1:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                break;
            case 2:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[2]);
                break;
            case 3:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[3]);
                break;
            case 4:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[4]);
                break;
            case 5:
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
                lastDialogue = true;
                break;
        }
        
    }
}

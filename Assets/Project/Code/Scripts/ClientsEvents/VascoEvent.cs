using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VascoEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private GameObject father;
    [SerializeField] private PlayerConversant playerConversant;

    private ClientNode client;
    private GameObject clientObject;

    private GameObject painting;
    private bool isSelected;
    private bool hasDialogueEnd;

    private void Update()
    {
        SetPainting();  
        if (client != null && client == eventClient && !isSelected && hasDialogueEnd)
        {
            if(painting != null)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                clientObject.GetComponent<AIConversant>().HandleDialogue();
                isSelected = true;
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                clientObject.GetComponent<AIConversant>().HandleDialogue();
                enabled = false;
            }

            
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(isSelected && painting == null)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
            clientObject.GetComponent<AIConversant>().HandleDialogue();
            enabled = false;
        }

        if (!hasDialogueEnd && playerConversant.IsActive() && clientObject.GetComponent<Client>().GetIsLocated() && client == eventClient)
        {
            if (!playerConversant.HasNext())
            {
                Debug.Log("si");
                hasDialogueEnd = true;
            }
        }

    }

    private void SetPainting()
    {
        if(father.transform.childCount == 0)
        {
            painting = null;
        }
        else
        {
            painting = father.transform.GetChild(0).gameObject;
        }
    }
}

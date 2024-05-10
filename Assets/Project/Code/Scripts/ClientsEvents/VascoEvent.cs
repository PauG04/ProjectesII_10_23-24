using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private bool paintBroken = false;

    private void Update()
    {
        SetPainting();  
        if (client != null && client == eventClient && !isSelected && hasDialogueEnd && TypeWriterEffect.isTextCompleted)
        {
            if(paintBroken && Input.GetMouseButton(0))
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                clientObject.GetComponent<AIConversant>().HandleDialogue(eventClient.pitch);
                isSelected = true;
            }
            else if(!paintBroken && Input.GetMouseButton(0))
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                clientObject.GetComponent<AIConversant>().HandleDialogue(eventClient.pitch);
                enabled = false;
            }

            
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(isSelected && !paintBroken && TypeWriterEffect.isTextCompleted && Input.GetMouseButton(0))
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
            clientObject.GetComponent<AIConversant>().HandleDialogue(eventClient.pitch);
            enabled = false;
        }

        if (!hasDialogueEnd && playerConversant.IsActive() && clientObject.GetComponent<Client>().GetIsLocated() && client == eventClient)
        {
            if (!playerConversant.HasNext())
            {
                hasDialogueEnd = true;
            }
        }

    }

    private void SetPainting()
    {
        if(father.transform.childCount == 0)
        {
            paintBroken = false;
        }
        else
        {
            if(father.transform.GetComponentInChildren<DragItems>().GetInsideWorkspace())
            {
                paintBroken = true;
                Debug.Log("entra");
            }
            else
            {
                paintBroken = false;
                Debug.Log("sale");
            }
            
        }
    }
}

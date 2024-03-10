using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseKeeperMatchDay : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;   
    [SerializeField] private List<ClientNode> catalonia;   
    [SerializeField] private List<Dialogue.Dialogue> dialogues;

    private ClientNode client;
    private GameObject clientObject;

    private bool triggerSetted = false;
    private bool served = false;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (served)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                if (!triggerSetted)
                {
                    clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("TakeMoney");
                    clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(TakeMoney);
                    triggerSetted = true;
                }
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
            }
            if (clientObject.GetComponent<BoxCollider2D>().enabled)
            {
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
                enabled = false;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if (catalonia[0] == client || catalonia[1] == client || catalonia[2] == client)
        {
            if(clientObject.GetComponent<Client>().GetWellReacted())
            {
                served = true;
            }
        }
    }

    public void TakeMoney()
    {
        EconomyManager.instance.SetMoneyChanged(-100);
    }
}

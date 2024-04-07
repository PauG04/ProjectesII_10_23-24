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
    [SerializeField] private float money;
    [SerializeField] private bool canSkip;

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
                    enabled = false;
                }
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                enabled = false;
            }
            if (clientObject.GetComponent<BoxCollider2D>().enabled)
            {
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
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

        if(served && !clientObject.GetComponent<Client>().GetWellReacted() && canSkip)
        {
            SkipCatalonians();
        }
        
    }

    private void SkipCatalonians()
    {
        for(int i = 0; i < catalonia.Count; i++)
        {
            if (client != null && client == catalonia[i])
            {
                ClientManager.instance.CreateClient();
                Destroy(clientObject);
            }
        }
    }

    public void TakeMoney()
    {
        EconomyManager.instance.SetMoneyChanged(-money);
    }

    public bool GetIsServed()
    {
        return served;
    }
}

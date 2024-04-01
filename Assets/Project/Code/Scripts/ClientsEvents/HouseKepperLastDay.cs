using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseKepperLastDay : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private PlayerConversant playerConversant;

    private ClientNode client;
    private GameObject clientObject;

    private bool triggerSetted = false;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(EconomyManager.instance.GetMoney() >= 300)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                if (!triggerSetted)
                {
                    clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("TakeMoney");
                    clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(TakeMoney);
                    triggerSetted = true;
                }
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                enabled = false;
            }


            if (clientObject && clientObject.GetComponent<BoxCollider2D>().enabled)
            {
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }


    }
    public void TakeMoney()
    {
        EconomyManager.instance.SetMoneyChanged(-300);
        enabled = false;
    }
}

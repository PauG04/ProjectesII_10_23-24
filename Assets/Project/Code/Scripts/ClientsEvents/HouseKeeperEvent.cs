using Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseKeeperEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private PlayerConversant playerConversant;

    private ClientNode client;
    private GameObject clientObject;

    private bool triggerSetted = false;

    private void Update()
    {
        if(client != null && client.clientName == "Casero")
        {
            if (!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("TakeMoney");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(TakeMoney);
                triggerSetted = true;
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
        EconomyManager.instance.SetMoneyChanged(-2000);
    }
}

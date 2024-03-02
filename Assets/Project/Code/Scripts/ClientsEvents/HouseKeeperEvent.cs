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

    private void Update()
    {
        if(client != null && client.clientName == "Casero")
        {
            if(playerConversant.GetCanContinue() && playerConversant.IsActive())
            {
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                    EconomyManager.instance.AddMoney(-2000);
                    enabled = false;
                }              
            }
            
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if (clientObject != null && clientObject.GetComponent<BoxCollider2D>().enabled)
        {
            clientObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}

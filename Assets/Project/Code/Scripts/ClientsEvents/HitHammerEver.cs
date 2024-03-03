using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHammerEver : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private PlayerConversant playerConversant;

    [Header("Time")]
    [SerializeField] private float maxTime;
    private float time; 

    private ClientNode client;
    private GameObject clientObject;

    private void Start()
    {
        time = 0;
    }

    private void Update()
    {
        if (client != null && client.clientName == "Mohammed")
        {
            if(playerConversant.GetCanContinue() && clientObject.GetComponent<Client>().GetIsLocated())
            { 
                StartTimer();
            }

            if (clientObject != null && clientObject.GetComponent<Client>().GetHitted())
            {
                enabled = false;
            }

        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
        
    }

    private void StartTimer()
    {
        if (playerConversant.HasNext())
        {
            time += Time.deltaTime;
            if (time > maxTime)
            {
                time = 0;
                playerConversant.Next();
            }
        }           
    }
}

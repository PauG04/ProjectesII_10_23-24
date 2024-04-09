using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHammerEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private PlayerConversant playerConversant;

    [Header("Time")]
    [SerializeField] private float maxTime;
    private float time; 

    private ClientNode client;
    private GameObject clientObject;

    [Header("ClientDialogueCollider")]
    [SerializeField] private BoxCollider2D clientDialogueCollider;

    private void Start()
    {
        time = 0;
    }

    private void Update()
    {
        if (client != null && client.clientName == "Mohammed")
        {
            if(playerConversant.GetChild() > 2 && clientObject.GetComponent<Client>().GetIsLocated())
            {
                clientDialogueCollider.enabled = false;
                StartTimer();
            }

            if (clientObject != null && clientObject.GetComponent<Client>().GetHitted())
            {
                clientDialogueCollider.enabled = true;
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

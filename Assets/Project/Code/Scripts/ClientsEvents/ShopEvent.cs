using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float cost;

    [SerializeField] private List<LiquidManager> botles;

    private ClientNode client;
    private GameObject clientObject;
    private bool isFirstButton = false;

    private bool triggerSetted = false;


    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted())
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("ResetDrink");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(ResetDrink);
                canvas.SetActive(false);
                enabled = false;
            }
            if (!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("ActiveCanvas");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(activeCanvas);
                triggerSetted = true;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void activeCanvas()
    {
        if(clientObject.GetComponent<Client>().GetHitted())
        {
            canvas.SetActive(true);
        }     
    }

    public void AcceptDeal()
    {
        if(!isFirstButton)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[3]);
            isFirstButton = true;
        }
        else
        {
            if(EconomyManager.instance.GetMoney() > cost)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[2]);
                EconomyManager.instance.SetMoneyChanged(-50);
                for(int i = 0; i< botles.Count; i++)
                {
                    botles[i].SetCurrentLiquid();
                }
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[4]);
            }
            
            clientObject.GetComponent<Client>().SetTimer(true);
            enabled = false;
        }
        clientObject.GetComponent<AIConversant>().HandleDialogue();
        canvas.SetActive(false);
        triggerSetted = false;
    }

    public void RejectDeal()
    {
        if(!isFirstButton)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
            isFirstButton = true;
        }
        else
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
            clientObject.GetComponent<Client>().SetTimer(true);
            enabled = false;
        }

        clientObject.GetComponent<AIConversant>().HandleDialogue();
        canvas.SetActive(false);
        triggerSetted = false;
    }

    public void ResetDrink()
    {
        for (int i = 0; i < 3; i++)
        {
            botles[i].SetCurrentLiquid();
        }
    }
}

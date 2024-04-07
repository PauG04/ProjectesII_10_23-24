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

    [SerializeField] private EndOfDay textDay;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(EconomyManager.instance.GetMoney() >= 170)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                textDay.SetMesage(true);
                if (playerConversant.GetChild() == 2)
                {
                    TakeMoney();
                }

            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                textDay.SetMesage(false);
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
        EconomyManager.instance.SetMoneyChanged(-170);
        enabled = false;
    }
}

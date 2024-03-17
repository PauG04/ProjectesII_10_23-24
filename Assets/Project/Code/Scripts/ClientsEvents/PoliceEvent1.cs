using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEvent1 : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float money;

    private ClientNode client;
    private GameObject clientObject;
    private bool triggerSetted = false;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (!triggerSetted && clientObject.GetComponent<Client>().GetBadReacted())
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("TakeMoney");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(TakeMoney);
                triggerSetted = true;
                enabled = false;
            }
            else if(clientObject.GetComponent<Client>().GetWellReacted())
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

    public void TakeMoney()
    {
        EconomyManager.instance.SetMoneyChanged(money);
    }
}

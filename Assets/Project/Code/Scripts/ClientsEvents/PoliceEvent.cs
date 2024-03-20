using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float totalHits;

    private ClientNode client;
    private GameObject clientObject;
    private float currentsHits;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted() && currentsHits <= totalHits)
            {
                EconomyManager.instance.SetMoneyChanged(-10);
                currentsHits++;
                clientObject.GetComponent<Client>().SetHitted(false);
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

      
    }

}

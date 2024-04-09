using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFather : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private ClientNode father;
    [SerializeField] private int position;

    private ClientNode client;
    private GameObject clientObject;


    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted() || clientObject.GetComponent<Client>().GetBadReacted())
            {
                clientManager.SetNewCLient(father, position);
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
}

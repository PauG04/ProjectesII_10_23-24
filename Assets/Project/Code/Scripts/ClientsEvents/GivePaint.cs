using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePaint : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private GameObject paint;
    [SerializeField] private Vector3 position;
    [SerializeField] private bool hasToTakeDrink;

    private bool postDrink;
    private ClientNode client;
    private GameObject clientObject;
    private bool triggerSetted = false;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (!triggerSetted)
            {
                if(!hasToTakeDrink)
                {
                    ActiveAction();
                }
                else if(hasToTakeDrink && postDrink)
                {
                    ActiveAction();
                }
                
            }

            if(clientObject.GetComponent<Client>().GetWellReacted())
            {
                postDrink = true;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

    }

    public void ActiveAction()
    {
        clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("GivePaint");
        clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(SpawnPaint);
        triggerSetted = true;
    }

    private void SpawnPaint()
    {
        GameObject painting = Instantiate(paint);
        painting.transform.localPosition = position;
        enabled = false;
    }
}

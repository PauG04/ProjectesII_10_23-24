using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValencianoEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private GameObject paint;
    [SerializeField] private GameObject father;

    private ClientNode client;
    private GameObject clientObject;
    private bool triggerSetted = false;
    private bool drugSpawned = false;
    private GameObject drug = null;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (!triggerSetted)
            {
                ActiveAction();
            }
            if (clientObject.GetComponent<Client>().GetWellReacted())
            {
                enabled = false;
            }
            if (drugSpawned && drug == null && !clientObject.GetComponent<Client>().GetWellReacted())
            {
                SpawnPaint();
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
        painting.transform.SetParent(father.transform, true);
        drug = painting;
        drugSpawned = true;
    }
}

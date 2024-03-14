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

    private ClientNode client;
    private GameObject clientObject;
    private bool triggerSetted = false;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("GivePaint");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(SpawnPaint);
                triggerSetted = true;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

    }

    private void SpawnPaint()
    {
        GameObject painting = Instantiate(paint);
        painting.transform.localPosition = position;
        enabled = false;
    }
}

using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private GameObject slotButton;

    private ClientNode client;

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            slotButton.SetActive(true);
            enabled = false;
        }
        else
        {
            client = clientManager.GetClient();
        }
    }
}

using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class StreamerEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private Dialogue.Dialogue dialogue;
    [SerializeField] private PlayerConversant player;
    [SerializeField] private float maxTime;
    [SerializeField] private float DesactiveVotationTime;
    [SerializeField] private GameObject canvas;

    private ClientNode client;
    private GameObject clientObject;

    private bool startVotation = false;
    private bool triggerSetted = false;

    private float time;
    private bool votation = false;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("StartVotation");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(StartVotation);
                triggerSetted = true;
            }

            if (startVotation)
            {
                Votation();
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void Votation()
    {
        time += Time.deltaTime;

        if(time > maxTime)
        {
            votation = false;
        }
        if(time > DesactiveVotationTime)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogue);
            clientObject.GetComponent<AIConversant>().HandleDialogue();
            clientObject.GetComponent<BoxCollider2D>().enabled = true;
            canvas.SetActive(false);
            enabled = false;
        }
    }

    private void StartVotation()
    {
        startVotation = true;
        votation = true;
        clientObject.GetComponent<BoxCollider2D>().enabled = false;
        canvas.SetActive(true);
    }

    public bool GetVotation()
    {
        return votation;
    }
}
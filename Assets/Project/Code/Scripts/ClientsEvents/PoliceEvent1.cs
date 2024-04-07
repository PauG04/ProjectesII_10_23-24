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
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private GameObject police;
    [SerializeField] private float velocity;

    [Header("ClientDialogueCollider")]
    [SerializeField] private BoxCollider2D clientDialogueCollider;

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
            }
            
            if(!clientObject.GetComponent<Client>().GetWellReacted() && !clientObject.GetComponent<Client>().GetBadReacted())
            {
                MoveSprites();
            }

            if(clientObject.GetComponent<Client>().GetBadReacted() || clientObject.GetComponent<Client>().GetWellReacted())
            {
                Reaction();
            }
            
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }


    }

    private void MoveSprites()
    {
        if(playerConversant.GetChild() == 2 && TypeWriterEffect.isTextCompleted)
        {
            clientDialogueCollider.enabled = false;
            if(police.transform.localPosition.x < -3.7)
            {
                police.transform.localPosition = new Vector3(police.transform.localPosition.x + velocity, 0.29f, 0);
            }
            else
            {
                clientDialogueCollider.enabled = true;
            }
        }
        if(playerConversant.GetChild() == 5)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -5;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        if (playerConversant.GetChild() == 6)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -1;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -5;
        }
        if (playerConversant.GetChild() == 7)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -5;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        if (playerConversant.GetChild() == 8)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -1;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -5;
        }
    }

    private void Reaction()
    {
        if (playerConversant.GetChild() == 0)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -1;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -5;
        }
        if (playerConversant.GetChild() == 1)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -5;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        if (playerConversant.GetChild() == 2)
        {
            police.GetComponent<SpriteRenderer>().sortingOrder = -1;
            clientObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -5;
        }
        if(clientObject.GetComponent<Client>().GetLeave())
        {
            police.GetComponent<ChildRun>().SetRunning(true);
            police.GetComponent<ChildRun>().SetCurrentY(0.29f);
            enabled = false;
        }
    }

    public void TakeMoney()
    {
        EconomyManager.instance.SetMoneyChanged(money);
    }
}

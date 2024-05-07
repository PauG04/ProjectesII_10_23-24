using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxCarnet : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float cost;

    [SerializeField] private GameObject item;
    [SerializeField] private GameObject father;

    private ClientNode client;
    private GameObject clientObject;
    private bool triggerSetted = false;

    private void Update()
    {
        if (client != null && client == eventClient && !GetComponent<HouseKeeperMatchDay>().GetIsServed())
        {
            if(!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("ActiveCanvas");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(ActiveCanvas);
                triggerSetted = true;
            }
            clientObject.GetComponent<Client>().SetLeaveAnimation(false);
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
        if(GetComponent<HouseKeeperMatchDay>().GetIsServed())
        {
            enabled = false;
        }
    }

    private void ActiveCanvas()
    {
        canvas.SetActive(true);
    }

    public void AcceptDeal()
    {
        if (EconomyManager.instance.GetMoney() > cost)
        {
            GameObject painting = Instantiate(item);
            painting.transform.SetParent(father.transform, true);
            painting.GetComponent<DragItems>().SetInitPosition(painting.transform.localPosition);
            EconomyManager.instance.SetMoneyChanged(-cost);
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
        }
        else
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[2]);
        }
        clientObject.GetComponent<AIConversant>().HandleDialogue(eventClient.pitch);
        canvas.SetActive(false);
        enabled = false;
    }

    public void RejectDeal()
    {
        clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
        clientObject.GetComponent<AIConversant>().HandleDialogue(eventClient.pitch);
        canvas.SetActive(false);
        enabled = false;
    }
}

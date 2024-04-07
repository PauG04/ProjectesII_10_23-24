using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitChildrenEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    private bool lastDialogue = false;

    [SerializeField] private GameObject[] childs;

    private ClientNode client;
    private GameObject clientObject;


    private void Update()
    {
        if (client != null && client == eventClient)
        { 
            if(!clientObject.GetComponent<Client>().GetIsLocated() && clientObject.GetComponent<Client>().GetArrive())
            {
                for(int i = 0; i< childs.Length; i++)
                {
                    if (!childs[i].activeSelf)
                    {
                        childs[i].SetActive(true);
                    }
                }
                childs[0].transform.localPosition = new Vector3(clientObject.transform.localPosition.x, -0.5f, -1);
                childs[1].transform.localPosition = new Vector3(clientObject.transform.localPosition.x - 0.6f, -0.5f, -1);
                childs[2].transform.localPosition = new Vector3(clientObject.transform.localPosition.x + 0.4f, -0.5f, -1);
                childs[3].transform.localPosition = new Vector3(clientObject.transform.localPosition.x - 1.5f, -0.5f, -1);
                childs[4].transform.localPosition = new Vector3(clientObject.transform.localPosition.x + 1.2f, -0.5f, -1);
            }
            if(clientObject.GetComponent<BoxCollider2D>().enabled)
            {
                SetDialogue();
            }          
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(lastDialogue)
        {
            if(!playerConversant.HasNext())
            {
                clientObject.GetComponent<Client>().SetLeaveAnimation(true);
                enabled = false;
            }
        }

    }

    public void SetDialogue()
    {
        switch (clientObject.GetComponent<Client>().GetCurrentsHit())
        {
            case 0:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
                break;
            case 1:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
                if (childs[0] != null)
                    childs[0].GetComponent<ChildRun>().SetRunning(true);
                break;
            case 2:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[2]);
                if (childs[1] != null)
                    childs[1].GetComponent<ChildRun>().SetRunning(true);
                break;
            case 3:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[3]);
                if (childs[2] != null)
                    childs[2].GetComponent<ChildRun>().SetRunning(true);
                break;
            case 4:
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[4]);
                if (childs[3] != null)
                    childs[3].GetComponent<ChildRun>().SetRunning(true);
                break;
            case 5:
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
                if (childs[4] != null)
                    childs[4].GetComponent<ChildRun>().SetRunning(true);
                lastDialogue = true;
                break;
        }
        
    }
}

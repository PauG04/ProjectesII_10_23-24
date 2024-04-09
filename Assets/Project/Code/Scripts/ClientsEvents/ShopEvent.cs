using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float cost;

    [SerializeField] private GameObject[] item;
    [SerializeField] private GameObject[] father;
    [SerializeField] private PlayerConversant player;

    private ClientNode client;
    private GameObject clientObject;
    private bool isFirstButton = false;

    private Vector2 position;


    private void Awake()
    {
        canvas.SetActive(false);
        position = new Vector2(-0.12f, 0.07f);
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (TypeWriterEffect.isTextCompleted && !player.HasNext() && clientObject.GetComponent<Client>().GetIsLocated())
            {
                ActiveCanvas();
            }
            if (clientObject.GetComponent<Client>().GetHitted())
            {
                ResetDrink(0);
                canvas.SetActive(false);
                enabled = false;
            }

        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void ActiveCanvas()
    {
        if(!clientObject.GetComponent<Client>().GetHitted())
        {
            canvas.SetActive(true);
        }     
    }

    public void AcceptDeal()
    {
        if(!isFirstButton)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[3]);
            isFirstButton = true;
        }
        else
        {
            if(EconomyManager.instance.GetMoney() > cost)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[2]);
                EconomyManager.instance.SetMoneyChanged(-cost);
                for(int i = 0; i< 3; i++)
                {
                    ResetDrink(i);
                }
            }
            else
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[4]);
            }
            
            clientObject.GetComponent<Client>().SetLeaveAnimation(true);
            enabled = false;
        }
        clientObject.GetComponent<AIConversant>().HandleDialogue();
        canvas.SetActive(false);
    }

    public void RejectDeal()
    {
        if(!isFirstButton)
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
            isFirstButton = true;
        }
        else
        {
            clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
            clientObject.GetComponent<Client>().SetLeaveAnimation(true);
            enabled = false;
        }

        clientObject.GetComponent<AIConversant>().HandleDialogue();
        canvas.SetActive(false);
    }

    public void ResetDrink(int index)
    {
        if (father[index].transform.childCount <= 1)
        {
            GameObject newItem = Instantiate(item[index]);

            if (father[index].transform.childCount == 1)
            {
                newItem.transform.SetParent(father[index].transform, true);
                Destroy(newItem.GetComponent<PolygonCollider2D>());
                newItem.GetComponent<SpriteRenderer>().color = Color.grey;
                newItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newItem.GetComponent<DragItems>().enabled = false;
                newItem.GetComponent<ArrowManager>().enabled = false;
                newItem.transform.GetChild(3).gameObject.SetActive(false);

                newItem.transform.localPosition = position;
            }
            else
            {
                newItem.transform.SetParent(father[index].transform, true);
                newItem.transform.localPosition = Vector2.zero;
                newItem.GetComponent<DragItems>().SetInitPosition(Vector2.zero);
            }
        }
            
    }
 
    
}

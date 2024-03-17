using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Lamps : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private Dialogue.Dialogue dialogue;

    private ClientNode client;
    private GameObject clientObject;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer lampSprite;
    private bool onLamp;
    private bool firsTime = false;
    private bool triggerSetted = false;

    private void Awake()
    {
        onLamp = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        lampSprite.sprite = sprites[1];
    }

    private void Update()
    {
        if (client != null)
        {
            if (client.invisible && !onLamp)
            {
                clientObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (client.invisible && onLamp)
            {
                clientObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }

            if (client == eventClient )
            {
                if (!triggerSetted)
                {
                    clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("Lamp");
                    clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(ActiveCollider);
                    triggerSetted = true;
                }
            }

        }
        if(clientObject == null)
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

    }

    private void OnMouseDown()
    {
        if(GetComponent<BoxCollider2D>().enabled)
        {
            onLamp = !onLamp;
            if (onLamp)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                lampSprite.sprite = sprites[3];

                if (client == eventClient && !firsTime)
                {
                    firsTime = true;
                    clientObject.GetComponent<AIConversant>().SetDialogue(dialogue);
                    clientObject.GetComponent<AIConversant>().HandleDialogue();
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                lampSprite.sprite = sprites[1];
            }


        }
    }

    private void ActiveCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


}

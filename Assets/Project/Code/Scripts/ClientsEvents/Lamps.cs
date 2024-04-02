using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Lamps : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private Dialogue.Dialogue dialogue;
    [SerializeField] private DayManager dayManager;

    private ClientNode client;
    private GameObject clientObject;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer lampSprite;

    [Header("Post Processing")]
    [SerializeField] private Volume volume;
    [SerializeField] private ColorAdjustments colorAdjustments;
    [SerializeField] private Vignette vignette;

    private bool onLamp;
    private bool firsTime = false;
    private bool triggerSetted = false;

    private void Awake()
    {
        onLamp = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        lampSprite.sprite = sprites[1];

        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out vignette);
        ActiveLights(false);

    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (client.invisible && !onLamp)
            {
                clientObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (client.invisible && onLamp)
            {
                clientObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
            if (!triggerSetted)
            {
                 clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("Lamp");
                 clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(ActiveCollider);
                 triggerSetted = true;
            }
           
        }
        if(clientObject == null && dayManager.GetCurrentDay() <= dayManager.GetLastDay())
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
            ActiveLights(onLamp);
            if (onLamp)
            {
                
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                lampSprite.sprite = sprites[3];

                if (client == eventClient && !firsTime)
                {
                    firsTime = true;
                    clientObject.GetComponent<AIConversant>().SetDialogue(dialogue);
                    clientObject.GetComponent<AIConversant>().HandleDialogue();
                    enabled = false;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                lampSprite.sprite = sprites[1];
            }


        }
    }

    private void ActiveLights(bool value)
    {
        vignette.active = value;
        colorAdjustments.active = value;
    }

    private void ActiveCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


}

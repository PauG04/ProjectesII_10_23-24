using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class CriptoBro : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private PlayerConversant player;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private List<Dialogue.Dialogue> dialogues;
    [SerializeField] private GameObject canvas;
    [SerializeField] private SpriteRenderer inversions;
    [SerializeField] private float velocity;

    private ClientNode client;
    private GameObject clientObject;
    private bool buttonPressed = false;

    private bool triggerSetted = false;


    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if (TypeWriterEffect.isTextCompleted && !player.HasNext() && clientObject.GetComponent<Client>().GetIsLocated())
            {
                activeCanvas();
            }
            if(buttonPressed && !player.HasNext())
            {
                clientObject.GetComponent<Client>().SetLeaveAnimation(true);
                inversions.gameObject.SetActive(false);
                enabled = false;
            }
            AlphaLerp();

        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void AlphaLerp()
    {
        if(inversions.color.a < 1 && clientObject.GetComponent<Client>().GetIsLocated())
        {
            Color newColor = inversions.color;

            newColor.a = Mathf.Lerp(newColor.a, 1, Time.deltaTime * velocity);

            inversions.color = newColor;
        }
    }

    private void activeCanvas()
    {
        if (!clientObject.GetComponent<Client>().GetHitted())
        {
            canvas.SetActive(true);
        }
    }

    public void AcceptDeal()
    {
        clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[0]);
        clientObject.GetComponent<AIConversant>().HandleDialogue();
        EconomyManager.instance.SetMoneyChanged(-20);
        canvas.SetActive(false);
        buttonPressed = true;
    }

    public void RejectDeal()
    {
        clientObject.GetComponent<AIConversant>().SetDialogue(dialogues[1]);
        clientObject.GetComponent<AIConversant>().HandleDialogue();
        canvas.SetActive(false);
        buttonPressed = true;
    }
}

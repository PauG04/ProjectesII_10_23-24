using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndaluzEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [Header("Event")]
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float IntensityShaking;
    [SerializeField] private Dialogue.Dialogue dialogue;
    [SerializeField] private PlayerConversant player;

    [SerializeField] private Sprite sleeping;
    [SerializeField] private Sprite currentSprite;

    private ClientNode client;
    private GameObject clientObject;
    private CameraShake _camera;

    private bool startShaking = false;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraShake>();
    }
    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted())
            {
                clientObject.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;
                _camera.SetTransforPosition();
                enabled = false;
            }
            if(clientObject.GetComponent<BoxCollider2D>().enabled && !startShaking && Input.GetMouseButtonDown(0) && TypeWriterEffect.isTextCompleted && !startShaking)
            {
                clientObject.GetComponent<AIConversant>().SetDialogue(dialogue);
                clientObject.GetComponent<AIConversant>().HandleDialogue();
                StartShaking();
                
            }
            shakingCamera();
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void StartShaking()
    {
        startShaking = true;
        clientObject.GetComponentInChildren<SpriteRenderer>().sprite = sleeping;
    }

    private void shakingCamera()
    {
        if(startShaking)
        {
            _camera.ShakeCamera(IntensityShaking);
        }
    }
}

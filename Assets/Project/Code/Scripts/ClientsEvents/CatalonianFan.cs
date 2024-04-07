using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CatalonianFan : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float intensityCamera;
    [SerializeField] private float shakeTime;
    [SerializeField] private List<GameObject> gool;
    [SerializeField] private float velocity;
    [SerializeField] private HouseKeeperMatchDay served;

    private ClientNode client;
    private GameObject clientObject;

    private bool triggerSetted = false;
    private bool screaming = false;

    private float time = 0;

    private void Awake()
    {
        for (int i = 0; i < gool.Count; i++)
        {
            gool[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }
    private void Update()
    {
        if(served.GetIsServed())
        {
            enabled = false;
        }
        if (client != null && client == eventClient)
        {
            if (!triggerSetted)
            {
                clientObject.GetComponent<DialogueTrigger>().SetTriggerAction("Goal");
                clientObject.GetComponent<DialogueTrigger>().SetOnTriggerEvent(Goal);
                triggerSetted = true;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(screaming)
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(intensityCamera);

            for (int i = 0; i < gool.Count; i++)
            {
                gool[i].GetComponent<SpriteRenderer>().color = Color.Lerp(gool[i].GetComponent<SpriteRenderer>().color, new Color(255, 255, 255, 1), Time.deltaTime * velocity);
            }

            time += Time.deltaTime;
            if(time > shakeTime)
            {
                Camera.main.GetComponent<CameraShake>().SetTransforPosition();
                screaming = false;
                for (int i = 0; i < gool.Count; i++)
                {
                    gool[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                }
                enabled = false;
            }
        }
    }

    public void Goal()
    {
        screaming = true;
        
    }
}

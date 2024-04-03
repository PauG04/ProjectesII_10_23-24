using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private PlayerConversant playerConversant;

    [Header("Lerp")]
    [SerializeField] private float velocityRotation;
    [SerializeField] private float velocityMove;

    private ClientNode client;
    private GameObject clientObject;

    private bool lerpActive = false;


    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted() || clientObject.GetComponent<Client>().GetWellReacted() && !lerpActive)
            {
                lerpActive = true;
                clientObject.GetComponent<Client>().SetLeaveAnimation(false);
                clientObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if(lerpActive && TypeWriterEffect.isTextCompleted)
        {
            ClientLerp();
        }

    }

    private void ClientLerp()
    {
        clientObject.transform.localRotation = Quaternion.Lerp(clientObject.transform.localRotation, new Quaternion(-1f, 0,0,1), Time.deltaTime * velocityRotation);
        clientObject.transform.localPosition = Vector3.Lerp(clientObject.transform.localPosition, new Vector3(
            clientObject.transform.localPosition.x,
            -0.4f, clientObject.transform.localPosition.z), Time.deltaTime * velocityMove);

        if(clientObject.transform.localPosition.y < -0.38)
        {
            ClientManager.instance.CreateClient();
            lerpActive = false;
            enabled = false;
        }
    }

}

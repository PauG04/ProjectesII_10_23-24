using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShakerStateMachine;

public class SibaritaEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float totalHits;

    [Header("Tutorial")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private BoxCollider2D shakerDrag;
    [SerializeField] private float velocity;
    private bool isGrowing;
    private int shakerOrderingLayerDrag;

    private ClientNode client;
    private GameObject clientObject;


    private void Awake()
    {
        isGrowing = true;
        shakerOrderingLayerDrag = shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetIsLocated())
            {
                shakerDrag.enabled = true;
            }
            if (playerConversant.GetCanContinue() && clientObject.GetComponent<Client>().GetIsLocated())
            {
                ActiveShakerItem();
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }


    }

    private void ActiveShakerItem()
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!shakerDrag.enabled)
        {
            isGrowing = true;
        }

        if (shakerDrag.gameObject.GetComponent<ShakerStateMachine>().GetCurrentState().StateKey != ShakerState.DraggingOpen && !shakerDrag.gameObject.GetComponent<ShakerStateMachine>().GetIsInWorkSpace())
        {
            panel.SetActive(true);
            shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 11;

            LerpSacele(maxScale, minScale, shakerDrag.gameObject.transform.GetChild(1).gameObject);
        }

        if (shakerDrag.gameObject.GetComponent<ShakerStateMachine>().GetCurrentState().StateKey == ShakerState.DraggingOpen)
        {
            panel.SetActive(false);

            shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = shakerOrderingLayerDrag;
            shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            shakerDrag.gameObject.transform.GetChild(1).transform.localScale = Vector3.one;
            if (shakerDrag.gameObject.GetComponent<ShakerStateMachine>().GetIsInWorkSpace())
            {
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                }
            }

        }
    }

    private void LerpSacele(Vector3 maxScale, Vector3 minScale, GameObject _object)
    {
        if (!isGrowing)
        {
            _object.transform.localScale = Vector3.Lerp(_object.transform.localScale, minScale, velocity * Time.deltaTime);
            if (_object.transform.localScale.x <= minScale.x + 0.02)
            {
                isGrowing = true;
            }
        }
        else if (isGrowing)
        {
            _object.transform.localScale = Vector3.Lerp(_object.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (_object.transform.localScale.x >= maxScale.x - 0.02)
            {
                isGrowing = false;
            }

        }
    }
}

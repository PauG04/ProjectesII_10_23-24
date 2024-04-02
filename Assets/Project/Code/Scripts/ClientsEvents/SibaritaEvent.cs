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

    [Header("Tutorial")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private BoxCollider2D shakerDrag;
    [SerializeField] private float velocity;
    [SerializeField] private SetTopShaker shakerTop;
    [SerializeField] private PolygonCollider2D shakerTopDrag;
    private bool isGrowing;
    private int shakerOrderingLayerDrag;
    private int dragOrderingLayerDrag;

    private ClientNode client;
    private GameObject clientObject;
    private bool[] tutorial;


    private void Awake()
    {
        isGrowing = true;
        shakerOrderingLayerDrag = shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
        dragOrderingLayerDrag = shakerTopDrag.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        tutorial = new bool[3];
        for(int i = 0; i<3; i++)
        {
            tutorial[i] = false;
        }

        tutorial[0] = true;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetIsLocated())
            {
                shakerDrag.enabled = true;
                shakerTopDrag.enabled = true;
            }
            if (playerConversant.GetCanContinue() && clientObject.GetComponent<Client>().GetIsLocated() && tutorial[0])
            {
                ActiveShakerItem();
            }
            if(playerConversant.GetCanContinue() && tutorial[1])
            {
                ActiveDragItem();
            }
            if(shakerTop.GetIsShakerClosed() && tutorial[2])
            {
                playerConversant.Next();
                enabled = false;
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
                tutorial[0] = false;
                tutorial[1] = true;
            }

        }
    }

    private void ActiveDragItem()
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!shakerTopDrag.gameObject.GetComponent<DragItems>().GetIsDraggin() && !shakerTopDrag.gameObject.GetComponent<DragItems>().GetInsideWorkspace())
        {
            panel.SetActive(true);
            if (shakerTopDrag.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                shakerTopDrag.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }

            LerpSacele(maxScale, minScale, shakerTopDrag.gameObject);
        }

        if (shakerTopDrag.gameObject.GetComponent<DragItems>().GetIsDraggin())
        {
            panel.SetActive(false);
            shakerTopDrag.gameObject.GetComponent<DragItems>().SetIsInTutorial(false);
            shakerTopDrag.gameObject.transform.localScale = Vector3.one;
            if (shakerTopDrag.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                shakerTopDrag.gameObject.GetComponent<SpriteRenderer>().sortingOrder = dragOrderingLayerDrag;
                shakerTopDrag.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
        }

        if (shakerTopDrag.gameObject.GetComponent<DragItems>().GetInsideWorkspace())
        {
            tutorial[1] = false;
            tutorial[2] = true;
            playerConversant.Next();
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
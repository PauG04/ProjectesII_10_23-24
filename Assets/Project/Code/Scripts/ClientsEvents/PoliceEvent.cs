using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PoliceEvent : MonoBehaviour
{
    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private ClientNode eventClient;
    [SerializeField] private float totalHits;

    [Header("Tutorial")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private PolygonCollider2D drag;
    [SerializeField] private float velocity;
    private bool isGrowing;
    private int initOrderingLayerDrag;

    private ClientNode client;
    private GameObject clientObject;
    private float currentsHits;

    [Header("ClientDialogueCollider")]
    [SerializeField] private BoxCollider2D clientDialogueCollider;

    private void Awake()
    {
        isGrowing = true;
        initOrderingLayerDrag = drag.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        if (client != null && client == eventClient)
        {
            if(clientObject.GetComponent<Client>().GetHitted() && currentsHits <= totalHits)
            {
                EconomyManager.instance.SetMoneyChanged(-10);
                currentsHits++;
                clientObject.GetComponent<Client>().SetHitted(false);
            }
            if(playerConversant.GetChild() == 7 && clientObject.GetComponent<Client>().GetIsLocated())
            {
                ActiveDragItem();
                clientDialogueCollider.enabled = false;
            }
            if(playerConversant.GetChild() > 7)
            { 
                clientDialogueCollider.enabled = true; 
            }
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

      
    }
    private void ActiveDragItem()
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!drag.gameObject.GetComponent<DragItems>().GetIsDraggin() && !drag.gameObject.GetComponent<DragItems>().GetInsideWorkspace())
        {
            panel.SetActive(true);
            if (drag.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }

            LerpSacele(maxScale, minScale, drag.gameObject);
        }

        if (drag.gameObject.GetComponent<DragItems>().GetIsDraggin())
        {
            panel.SetActive(false);
            drag.gameObject.GetComponent<DragItems>().SetIsInTutorial(false);
            drag.gameObject.transform.localScale = Vector3.one;
            if (drag.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag.gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerDrag;
                drag.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
        }

        if(drag.gameObject.GetComponent<DragItems>().GetInsideWorkspace())
        {
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

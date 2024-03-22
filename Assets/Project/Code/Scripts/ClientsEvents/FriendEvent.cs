using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShakerStateMachine;
using static UnityEngine.Rendering.DebugUI;

public class FriendEvent : MonoBehaviour
{
    [Header("DragScripts")]
    [SerializeField] private List<PolygonCollider2D> drag;
    [SerializeField] private BoxCollider2D shakerDrag;
    private List<int> initOrderingLayerDrag;
    private int shakerOrderingLayerDrag;

    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private NextButton button;

    private ClientNode client;
    [SerializeField] private GameObject clientObject;

    private bool startTutorial;
    private bool[] tutorialBooleans;
  
    [Header("Arrow")]
    [SerializeField] private List<GameObject> arrow;
    private Vector3[] initArrowPosition;
    private bool[] isRight;

    [Header("Glass")]
    [SerializeField] private List<GameObject> createGlass;
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject glassLiquid;

    [Header("Lerp")]
    [SerializeField] private float velocity;
    bool isGrowing;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    [Header("OpenFridge")]
    [SerializeField] private OpenFridge fridge;
    private int initOrderingLayerFridge;

    [Header("CreateObject")]
    [SerializeField] private BoxCollider2D createObjectCollider;
    [SerializeField] private GameObject ice;
    private int initOrderingLayerBucket;

    private int dialogues;

    [SerializeField] private WikiManager wiki;

    private void Start()
    {
        startTutorial = false;

        tutorialBooleans = new bool[25];
        initOrderingLayerDrag = new List<int>(drag.Count);

        for (int i = 0; i < drag.Count; i++)
        {
            drag[i].enabled = false;
            drag[i].gameObject.GetComponent<DragItems>().SetIsInTutorial(true);

            if (drag[i].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                initOrderingLayerDrag.Add(drag[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }
        }

        for (int i = 0; i < tutorialBooleans.Length; i++)
        {
            tutorialBooleans[i] = false;
        }

        for (int i = 0; i < createGlass.Count; i++)
        {
            createGlass[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        initArrowPosition = new Vector3[4];
        isRight = new bool[4];
        for(int i = 0; i< initArrowPosition.Length; i++)
        {
            initArrowPosition[i] = arrow[i].transform.position;
            
        }
        for (int i = 0; i < isRight.Length; i++)
        {
            if (arrow[i].transform.rotation.z == 0)
                isRight[i] = true;
            else
                isRight[i] = false;

        }
        shakerDrag.enabled = false;
        shakerDrag.GetComponent<ShakerStateMachine>().SetIsInTutorial(true);

        shakerOrderingLayerDrag = shakerDrag.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;

        initOrderingLayerFridge = fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        fridge.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        fridge.enabled = false;

        initOrderingLayerBucket = createObjectCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        createObjectCollider.enabled = false;

        tutorialBooleans[0] = true;

    }

    private void Update()
    {
        SetDrag();
        SetClient();
        
        for (int i = 0; i < arrow.Count; i++)
        {
            ActiveArrow(i);
        }
    }

    private void SetClient()
    {
        if (client != null && client.clientName == "Amigo")
        {
            Tutorial();
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }

        if (!tutorialBooleans[14] && clientObject != null)
        {
            clientObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (tutorialBooleans[15])
        {
            clientObject.GetComponent<BoxCollider2D>().enabled = true;

            for (int i = 1; i < createGlass.Count; i++)
            {
                createGlass[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            for (int i = 0; i < drag.Count; i++)
            {
                if (!drag[i].enabled)
                {
                    drag[i].enabled = true;
                    drag[i].GetComponent<DragItems>().SetIsInTutorial(false);
                }
            }
            enabled = false;
        }
    }

    private void SetDrag()
    {
        for (int i = 0; i < drag.Count; i++)
        {
            if (!drag[i].enabled && drag[i] != null)
            {
                if (drag[i].gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    drag[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }
                else
                {
                    drag[i].gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }
            }

        }
    }

    private void Tutorial()
    {
        if(!startTutorial && clientObject.GetComponent<Client>().GetIsLocated())
        {
            button.Active();

            startTutorial = true;
        }

        if(!button.GetComponent<SpriteRenderer>().enabled && startTutorial)
        {
            ActiveItem();
        }
    }

    private void ActiveItem()
    {
        if (tutorialBooleans[0])
        {
            ActiveDragItem(3, 0);
            dialogues = 1;
        }
        else if (tutorialBooleans[1] && drag[3].GetComponent<DragItems>().GetWasOnTheTable())
        {
            if (playerConversant.HasNext() && dialogues > 0)
            {
                playerConversant.Next();
                dialogues--;
            } 

            if(wiki.GetCurrentPage() == 0)
            {
                tutorialBooleans[2] = true;
                tutorialBooleans[1] = false;
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                }
                button.Active();
            }
        }
        else if(tutorialBooleans[2])
        {
            if(!button.GetIsActive())
            {
                ActiveShakerItem(2);
            }
        }
        else if (tutorialBooleans[3])
        {
            if (!button.GetIsActive())
            {             
                ActiveDragItem(5, 3);
            }
        }
        else if (tutorialBooleans[4])
        {
            if (!button.GetIsActive())
            {
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                }
                button.Active();
                tutorialBooleans[4] = false;
                tutorialBooleans[5] = true;
            }
        }
        else if (tutorialBooleans[5])
        {
            if (!button.GetIsActive())
            {
                ActiveCreateGlass(0, 5);
            }
        }
        else if (tutorialBooleans[6])
        {
            ActiveFridge(6);
        }
        else if (tutorialBooleans[7])
        {
            ActiveCreateObjectFridge(7);
        }
        else if (tutorialBooleans[8])
        {
            if(!fridge.GetIsOpen())
            {
                ActiveDragItem(0, 8);
            }

        }
        else if (ice != null && ice.GetComponent<BreakIce>().GetIceDropped() > 1 && tutorialBooleans[9])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[9] = false;
            tutorialBooleans[10] = true;
        }
        else if(glass != null && glass.GetComponent<InsideDecorations>().GetIceInside() == 2 && tutorialBooleans[10])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[10] = false;
            tutorialBooleans[11] = true;
        }
        else if (tutorialBooleans[11])
        {
            ActiveDragItem(1, 11);
        }
        else if (glassLiquid != null && glassLiquid.GetComponent<LiquidManager>().GetCurrentLiquid() >= glassLiquid.GetComponent<LiquidManager>().GetMaxLiquid() / 4 && tutorialBooleans[12])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[12] = false;
            tutorialBooleans[13] = true;
        }
        else if (tutorialBooleans[13])
        {
            ActiveDragItem(2, 13);
        }
        else if (glassLiquid != null && glassLiquid.GetComponent<LiquidManager>().GetCurrentLiquid() >= glassLiquid.GetComponent<LiquidManager>().GetMaxLiquid() / 1.25f && tutorialBooleans[14])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
                tutorialBooleans[14] = true;
                tutorialBooleans[15] = true;
            }
        }
    }

    private void ActiveDragItem(int _index, int boolIndex)
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!drag[_index].enabled)
        {
            drag[_index].enabled = true;
            isGrowing = true;
        }

        if (drag[_index].gameObject.GetComponent<DragItems>().GetWasOnTheTable())
        {
            drag[_index].gameObject.GetComponent<DragItems>().SetIsInTutorial(false);
        }
        if (!drag[_index].gameObject.GetComponent<DragItems>().GetIsDraggin() && !drag[_index].gameObject.GetComponent<DragItems>().GetInsideWorkspace())
        {
            panel.SetActive(true);
            if (drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }

            LerpSacele(maxScale, minScale, drag[_index].gameObject);
        }

        if (drag[_index].gameObject.GetComponent<DragItems>().GetIsDraggin())
        {
            panel.SetActive(false);
            drag[_index].gameObject.GetComponent<DragItems>().SetIsInTutorial(false);
            drag[_index].gameObject.transform.localScale = Vector3.one;
            if (drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerDrag[_index];
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
            tutorialBooleans[boolIndex] = false;
            tutorialBooleans[boolIndex + 1] = true;
        }
    }
    private void ActiveShakerItem(int boolIndex)
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!shakerDrag.enabled)
        {
            shakerDrag.enabled = true;
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
                tutorialBooleans[boolIndex] = false;
                tutorialBooleans[boolIndex + 1] = true;
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                }
                button.Active();

            }

        }
    }

    private void ActiveCreateGlass(int _index, int boolIndex)
    {
        Vector3 maxScale = new Vector3(1.25f, 1.25f, 1.6f);
        Vector3 minScale = new Vector3(0.55f, 0.55f, 0.8f);

        if (!createGlass[_index].GetComponent<BoxCollider2D>().enabled)
        {
            createGlass[_index].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isGrowing = true;
        }
        if (!createGlass[_index].GetComponent<SpawnGlass>().GetIsCreated())
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, createGlass[_index]);
            createGlass[_index].GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        if (createGlass[_index].GetComponent<SpawnGlass>().GetIsCreated())
        {
            panel.SetActive(false);
            createGlass[_index].GetComponent<SpriteRenderer>().sortingOrder = 1;
            createGlass[_index].transform.localScale = new Vector3(0.75f, 0.75f, 1);
            if (playerConversant.HasNext())
            {
                playerConversant.Next();               
            }
            tutorialBooleans[boolIndex] = false;
            tutorialBooleans[boolIndex + 1] = true;
        }
    }

    private void ActiveFridge(int boolIndex)
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!fridge.enabled)
        {
            fridge.enabled = true;
            fridge.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isGrowing = true;
        }

        if (!fridge.GetIsOpen())
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, fridge.gameObject);
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        if (fridge.GetIsOpen())
        {
            panel.SetActive(false);
            fridge.gameObject.transform.localScale = Vector3.one;
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerFridge;
            tutorialBooleans[boolIndex] = false;
            tutorialBooleans[boolIndex + 1] = true;
        }
    }

    private void ActiveCreateObjectFridge(int boolIndex)
    {
        Vector3 maxScale = new Vector3(2.1f, 2.1f, 2.1f);
        Vector3 minScale = new Vector3(1.9f, 1.9f, 1.9f);

        if (!createObjectCollider.enabled)
        {
            createObjectCollider.enabled = true;
            isGrowing = true;
        }
        if (!createObjectCollider.gameObject.GetComponent<CreateItemGroup>().GetIsCreated())
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, createObjectCollider.gameObject);
            createObjectCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        if (createObjectCollider.gameObject.GetComponent<CreateItemGroup>().GetIsCreated())
        {
            panel.SetActive(false);
            createObjectCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerBucket;
            createObjectCollider.gameObject.transform.localScale = new Vector3(2, 2, 2);
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[boolIndex] = false;
            tutorialBooleans[boolIndex + 1] = true;
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

    private void ActiveArrow(int index)
    {
        if (!drag[3].GetComponent<DragItems>().GetWasOnTheTable() && drag[3].GetComponent<DragItems>().GetIsDraggin())
        {
            arrow[index].SetActive(true);
            if (isRight[index])
            {
                Vector3 newPosition = arrow[index].transform.position;
                newPosition.x = Mathf.Lerp(arrow[index].transform.position.x, initArrowPosition[index].x + 0.5f, Time.deltaTime * 5);
                arrow[index].transform.position = newPosition;
                if (arrow[index].transform.position.x >= initArrowPosition[index].x + 0.4f)
                {
                    isRight[index] = false;
                }
            }
            else
            {
                Vector3 newPosition = arrow[index].transform.position;
                newPosition.x = Mathf.Lerp(arrow[index].transform.position.x, initArrowPosition[index].x - 0.5f, Time.deltaTime * 5);
                arrow[index].transform.position = newPosition;
                if (arrow[index].transform.position.x <= initArrowPosition[index].x - 0.4f)
                {
                    isRight[index] = true;
                }
            }
        }
        else
        {
            arrow[index].SetActive(false);
        }
    }

    public void SetIce(GameObject item)
    {
        ice = item;
    }

    public void SetGlass(GameObject _glass)
    {
        glass = _glass;
        glassLiquid = _glass.transform.parent.transform.GetChild(2).gameObject;
    }
}

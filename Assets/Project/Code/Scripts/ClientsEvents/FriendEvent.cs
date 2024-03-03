using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FriendEvent : MonoBehaviour
{
    [Header("DragScripts")]
    [SerializeField] private List<DragItems> drag;
    private List<int> initOrderingLayerDrag;

    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private NextButton button;

    private ClientNode client;
    [SerializeField] private GameObject clientObject;

    private bool startTutorial;
    private float[] time;
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

    private void Awake()
    {
        startTutorial = false;

        time = new float[10];
        tutorialBooleans = new bool[11];
        initOrderingLayerDrag = new List<int>(drag.Count);

        for (int i = 0; i < drag.Count; i++)
        {
            drag[i].enabled = false;

            if (drag[i].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                initOrderingLayerDrag.Add(drag[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }
        }

        for (int i = 0; i < time.Length; i++)
        {
            time[i] = 0;
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

        initOrderingLayerFridge = fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        fridge.enabled = false;

        initOrderingLayerBucket = createObjectCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        createObjectCollider.enabled = false;

        tutorialBooleans[0] = true;
    }

    private void Update()
    {
        SetClient();
        SetDrag();

        for(int i = 0; i < arrow.Count; i++)
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

        if (!tutorialBooleans[10] && clientObject != null)
        {
            clientObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            clientObject.GetComponent<BoxCollider2D>().enabled = true;
            
            for (int i = 1; i < createGlass.Count; i++)
            {
                createGlass[i].GetComponent<BoxCollider2D>().enabled = true;
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
            ActiveCreateGlass(0, 0, 0, 5);
        }
        else if (tutorialBooleans[1])
        {
            ActiveFridge(1, 1, 5);
        }
        else if(tutorialBooleans[2])
        {
            ActiveCreateObjectFridge(2, 2, 5);
        }
        else if (tutorialBooleans[3])
        {
            ActiveDragItem(0, 3, 3, 5);
        }
        else if(ice != null && ice.GetComponent<BreakIce>().GetIceDropped() > 1 && tutorialBooleans[4])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[4] = false;
            tutorialBooleans[5] = true;
        }
        else if(glass != null && glass.GetComponent<InsideDecorations>().GetIceInside() == 2 && tutorialBooleans[5])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[5] = false;
            tutorialBooleans[6] = true;
        }
        else if (tutorialBooleans[6])
        {
            ActiveDragItem(1, 4, 6, 5);
        }
        else if (glassLiquid != null && glassLiquid.GetComponent<LiquidManager>().GetCurrentLiquid() >= glassLiquid.GetComponent<LiquidManager>().GetMaxLiquid() / 2 && tutorialBooleans[7])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[7] = false;
            tutorialBooleans[8] = true;
        }
        else if (tutorialBooleans[8])
        {
            ActiveDragItem(2, 5, 8, 5);
        }
        else if (glassLiquid != null && glassLiquid.GetComponent<LiquidManager>().GetCurrentLiquid() >= glassLiquid.GetComponent<LiquidManager>().GetMaxLiquid() && tutorialBooleans[9])
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            tutorialBooleans[9] = false;
            tutorialBooleans[10] = true;
        }

    }

    private void ActiveDragItem(int _index, int timeIndex, int boolIndex, float _maxTime)
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);
        time[timeIndex] += Time.deltaTime;

        if (!drag[_index].enabled)
        {
            drag[_index].enabled = true;
            drag[_index].SetIsInTutorial(true);
            isGrowing = true;
        }

        if (drag[_index].GetWasOnTheTable())
        {
            drag[_index].SetIsInTutorial(false);
        }
        if (!drag[_index].GetIsDraggin() && !drag[_index].GetInsideWorkspace() && time[timeIndex] > _maxTime)
        {
            panel.SetActive(true);
            if (drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }

            LerpSacele(maxScale, minScale, drag[_index].gameObject);
        }

        if (drag[_index].GetIsDraggin())
        {
            panel.SetActive(false);
            drag[_index].SetIsInTutorial(false);
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

    private void ActiveCreateGlass(int _index, int timeIndex, int boolIndex, float _maxTime)
    {
        Vector3 maxScale = new Vector3(1.25f, 1.25f, 1.6f);
        Vector3 minScale = new Vector3(0.55f, 0.55f, 0.8f);

        time[timeIndex] += Time.deltaTime;

        if (!createGlass[_index].GetComponent<BoxCollider2D>().enabled)
        {
            createGlass[_index].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isGrowing = true;
        }
        if (!createGlass[_index].GetComponent<SpawnGlass>().GetIsCreated() && time[timeIndex] > _maxTime)
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
            button.Active();
        }
    }

    private void ActiveFridge(int timeIndex, int boolIndex, float _maxTime)
    {
        Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        time[timeIndex] += Time.deltaTime;


        if (!fridge.enabled)
        {
            fridge.enabled = true;
            fridge.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isGrowing = true;
        }

        if (!fridge.GetIsOpen() && time[timeIndex] > _maxTime)
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

    private void ActiveCreateObjectFridge(int timeIndex, int boolIndex, float _maxTime)
    {
        Vector3 maxScale = new Vector3(2.1f, 2.1f, 2.1f);
        Vector3 minScale = new Vector3(1.9f, 1.9f, 1.9f);

        time[timeIndex] += Time.deltaTime;


        if (!createObjectCollider.enabled)
        {
            createObjectCollider.enabled = true;
            isGrowing = true;
        }
        if (!createObjectCollider.gameObject.GetComponent<CreateItemGroup>().GetIsCreated() && time[timeIndex] > _maxTime)
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
        if (isGrowing)
        {
            _object.transform.localScale = Vector3.Lerp(_object.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (_object.transform.localScale.x >= maxScale.x - 0.01)
            {
                isGrowing = false;
            }
        }
        else
        {
            _object.transform.localScale = Vector3.Lerp(_object.transform.localScale, minScale, velocity * Time.deltaTime);
            if (_object.transform.localScale.x <= minScale.x + 0.01)
            {
                isGrowing = true;
            }
        }
    }

    private void ActiveArrow(int index)
    {
        if(glass != null && !glass.transform.parent.GetComponent<DragItems>().GetWasOnTheTable())
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

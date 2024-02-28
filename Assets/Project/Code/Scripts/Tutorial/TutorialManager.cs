using Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.VolumeComponent;

public class TutorialManager : MonoBehaviour
{
    [Header("DragScripts")]
    [SerializeField] private List<DragItems> drag;
    private List<int> initOrderingLayerDrag;
    //[SerializeField] private SpriteRenderer glass;

    [Header("Jigger")]
    [SerializeField] private RotateBottle jigger;
    [SerializeField] private LiquidManager jiggerLiquid;

    [Header("Shaker")]
    [SerializeField] private ShakerStateMachine shaker;
    [SerializeField] private SpriteRenderer shakerSpiteRenderer;
    [SerializeField] private LiquidManager shakerLiquid;

    [Header("CreateObject")]
    [SerializeField] private List<BoxCollider2D> createObjectCollider;
    [SerializeField] private GameObject createGlass;
    private List<int> initOrderingLayerBucket;

    [Header("OpenFridge")]
    [SerializeField] private OpenFridge fridge;
    private int initOrderingLayerFridge;

    [Header("Lerp")]
    [SerializeField] private float velocity;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    [SerializeField] private PlayerConversant playerConversant;
    private bool startTutorial;
    private bool isFriend;

    [SerializeField] private GameObject clientIndex;
    [SerializeField] private GameObject client;

    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject arrow;
    private Vector3 initArrowPosition;
    private bool isRight;

    private bool[] continuConversation;
    private float[] time;

    private GameObject glass;

    private bool isButtonActive;

    [SerializeField] private GameObject nextButton;

    private enum tutorialID
    {
        NotTutorial,
        BaseTutorial,
        GlassTutorial,
        MinigameTutorial
    }

    [SerializeField] private tutorialID id;

    bool isGrowing;

    private void Awake()
    {
        initOrderingLayerDrag = new List<int>(drag.Count);
        initOrderingLayerBucket = new List<int>(createObjectCollider.Count);

        for (int i = 0; i < drag.Count; i++)
        {
            drag[i].enabled = false;

            if (drag[i].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                initOrderingLayerDrag.Add(drag[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }
            else
            {
                //initOrderingLayerDrag.Add(glass.sortingOrder);
            }
        }
        for (int i = 0; i < createObjectCollider.Count; i++)
        {
            createObjectCollider[i].enabled = false;
            initOrderingLayerBucket.Add(createObjectCollider[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }
        initOrderingLayerFridge = fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

        shaker.enabled = false;
        jigger.enabled = false;
        fridge.enabled = false;
        shakerLiquid.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        fridge.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        createGlass.gameObject.GetComponent<BoxCollider2D>().enabled = false;


        panel.SetActive(false);
        startTutorial = false;
        continuConversation = new bool[10];
        time = new float[10];
        for (int i = 0; i < continuConversation.Length; i++)
        {
            continuConversation[i] = true;
            time[i] = 0;
        }

        isFriend = false;

        initArrowPosition = arrow.transform.position;
        isRight = true;
        arrow.SetActive(false);

        isButtonActive = false;
    }

    private void Update()
    {
        ActivateTutorial();

        for (int i = 0; i < drag.Count; i++)
        {
            if (!drag[i].enabled && drag[i] != null)
            {
                drag[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
        if (!shaker.enabled)
        {
            shaker.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        if (!drag[2].GetWasOnTheTable())
        {
            shakerLiquid.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (drag[0].enabled)
        {
            ActiveArrow();
        }
        
    }

    private void ActivateTutorial()
    {
        if (isFriend && !startTutorial)
        {
            startTutorial = true;
            client = clientIndex.transform.GetChild(3).gameObject;
            client.GetComponent<BoxCollider2D>().enabled = false;

        }

        if(isFriend && client.GetComponent<Client>().GetIsLocated() && !isButtonActive)
        {
            nextButton.GetComponent<NextButton>().Active();
            isButtonActive = true;
        }

        if (id == tutorialID.BaseTutorial && !nextButton.GetComponent<SpriteRenderer>().enabled && startTutorial)
        {
            CallActiveDrag();
        }
    }

    private void CallActiveDrag()
    {
        if (ice != null && ice.GetComponent<BreakIce>().GetIceDropped() > 1)
        {
            ContinueConversation();
            client.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (createObjectCollider[0].gameObject.GetComponent<CreateObject>().GetIsCreated() && !fridge.GetIsOpen())
        {
            createObjectCollider[0].gameObject.transform.localScale = new Vector3(2, 2, 2);
            ActiveDragItem(5, 5, 9);
        }
        else if (fridge.GetIsOpen() && continuConversation[9])
        {
            continuConversation[8] = true;
            fridge.gameObject.transform.localScale = Vector3.one;
            ActiveCreateObjectFridge(0, 5, 8);
            if (createObjectCollider[0].gameObject.GetComponent<CreateObject>().GetIsCreated())
            {
                ContinueConversation();
                continuConversation[9] = false;
            }
        }
        else if (!fridge.GetIsOpen() && !continuConversation[8])
        {
            ActiveFridge(5, 7);
        }
        else if (shakerLiquid.GetDrinkState() == CocktailNode.State.Mixed && !continuConversation[7])
        {
            ActiveCreateGlass(5, 6);
            if(glass != null)
            {
                if (glass.gameObject.transform.GetChild(2).GetComponent<LiquidManager>().GetCurrentLiquid() >= glass.gameObject.transform.GetChild(2).GetComponent<LiquidManager>().GetMaxLiquid() * 0.7)
                {
                    ContinueConversation();
                    continuConversation[7] = true;
                    continuConversation[8] = false;
                    nextButton.GetComponent<NextButton>().Active();
                }
            }
            
        }
        else if (shakerLiquid.GetCurrentLiquid() >= shakerLiquid.GetMaxLiquid() && !continuConversation[6])
        {
            ActiveDragItem(4, 5, 5);
            if (shakerLiquid.GetDrinkState() == CocktailNode.State.Mixed)
            {
                ContinueConversation();
                continuConversation[6] = true;
                continuConversation[7] = false;
            }
        }
        else if (shakerLiquid.GetCurrentLiquid() >= jiggerLiquid.GetMaxLiquid() * 0.7 && !continuConversation[5])
        {
            ActiveDragItem(3, 5, 4);
            if (shakerLiquid.GetCurrentLiquid() >= shakerLiquid.GetMaxLiquid())
            {
                ContinueConversation();
                continuConversation[5] = true;
                continuConversation[6] = false;
                nextButton.GetComponent<NextButton>().Active();
            }
        }
        else if (drag[2].GetWasOnTheTable() && !continuConversation[4])
        {
            if (jiggerLiquid.GetCurrentLiquid() >= jiggerLiquid.GetMaxLiquid())
            {
                shakerLiquid.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (shakerLiquid.GetCurrentLiquid() >= jiggerLiquid.GetMaxLiquid() * 0.7)
            {
                ContinueConversation();
                continuConversation[5] = false;
            }
        }
        else if (drag[1].GetWasOnTheTable() && !continuConversation[3])
        {

            ActiveDragItem(2, 5, 3);
            if (drag[2].GetWasOnTheTable() && continuConversation[4])
            {
                ContinueConversation();
                continuConversation[4] = false;
                nextButton.GetComponent<NextButton>().Active();
            }
        }
        else if (shaker.GetWasInTable() && !continuConversation[2])
        {
            ActiveDragItem(1, 5, 2);
            jigger.enabled = true;
            if (drag[1].GetWasOnTheTable() && continuConversation[3])
            {
                ContinueConversation();
                continuConversation[3] = false;
            }
        }
        else if (drag[0].GetWasOnTheTable() && !continuConversation[0])
        {
            if (shaker.GetWasInTable() && continuConversation[1])
            {
                ContinueConversation();
                continuConversation[1] = false;
                continuConversation[2] = false;
                shaker.SetIsInTutorial(false);
            }
            else
            {
                ActiveDragShaker(2.5f, 1);
            }

        }
        else
        {
            ActiveDragItem(0,2.5f, 0);
            if (drag[0].GetWasOnTheTable() && continuConversation[0])
            {
                ContinueConversation();
                continuConversation[0] = false;
                nextButton.GetComponent<NextButton>().Active();
            }
        }
    }

    private void ActiveDragShaker(float _maxTime, int timeIndex)
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        time[timeIndex] += Time.deltaTime;

        if (!shaker.enabled)
        {
            shaker.enabled = true;
            shaker.SetIsInTutorial(true);

            isGrowing = true;
        }

        if (shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingOpen && shaker.GetIsInTutorial() && !shaker.GetIsInWorkSpace() && time[timeIndex] > _maxTime)
        {
            panel.SetActive(true);
            shakerSpiteRenderer.sortingOrder = 11;
            LerpSacele(maxScale, minScale, shaker.gameObject);
        }

        if (shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleOpen && shaker.GetIsInTutorial())
        {
            panel.SetActive(false);
        }

    }

    private void ActiveDragItem(int _index, float _maxTime, int timeIndex)
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
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
        if (!drag[_index].GetIsDraggin() && drag[_index].GetIsInTutorial() && !drag[_index].GetInsideWorkspace() && time[timeIndex] > _maxTime)
        {
            panel.SetActive(true);
            if (drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }
            else
            {
                //glass.sortingOrder = 11;
            }

            LerpSacele(maxScale, minScale, drag[_index].gameObject);
        }

        if (drag[_index].GetIsDraggin() && drag[_index].GetIsInTutorial())
        {
            panel.SetActive(false);
            drag[_index].gameObject.transform.localScale = Vector3.one;
            if (drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerDrag[_index];
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
            else
            {
                //glass.sortingOrder = initOrderingLayerDrag[_index];
                //glass.sortingLayerName = "Default";
            }

        }
    }

    private void ActiveFridge(float _maxTime, int timeIndex)
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
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
        }
    }

    private void ActiveCreateObjectFridge(int index, float _maxTime, int timeIndex)
    {
        Vector3 maxScale = new Vector3(2.1f, 2.1f, 2.1f);
        Vector3 minScale = new Vector3(1.9f, 1.9f, 1.9f);

        time[timeIndex] += Time.deltaTime;


        if (!createObjectCollider[index].enabled)
        {
            createObjectCollider[index].enabled = true;
            isGrowing = true;
        }
        if (!createObjectCollider[index].gameObject.GetComponent<CreateObject>().GetIsCreated() && time[timeIndex] > _maxTime)
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, createObjectCollider[index].gameObject);
            createObjectCollider[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        if (createObjectCollider[index].gameObject.GetComponent<CreateObject>().GetIsCreated())
        {
            panel.SetActive(false);
            createObjectCollider[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerBucket[index];
            createObjectCollider[index].gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
    }

    private void ActiveCreateGlass(float _maxTime, int timeIndex)
    {
        Vector3 maxScale = new Vector3(0.95f, 0.95f, 1.2f);
        Vector3 minScale = new Vector3(0.55f, 0.55f, 0.8f);

        time[timeIndex] += Time.deltaTime;


        if (!createGlass.GetComponent<BoxCollider2D>().enabled)
        {
            createGlass.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isGrowing = true;
        }
        if (!createGlass.gameObject.GetComponent<SpawnGlass>().GetIsCreated() && time[timeIndex] > _maxTime)
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, createGlass.gameObject);
            createGlass.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        if (createGlass.gameObject.GetComponent<SpawnGlass>().GetIsCreated())
        {
            panel.SetActive(false);
            createGlass.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            createGlass.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 1);
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

    private void ContinueConversation()
    {
        if (playerConversant.GetCanContinue())
        {
            //playerConversant.SetCanContinue(true);
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
        }
    }

    private void ActiveArrow()
    {
        if (drag[0].GetIsDraggin() && !drag[0].GetWasOnTheTable())
        {
            arrow.SetActive(true);
            if (isRight)
            {
                Vector3 newPosition = arrow.transform.position;
                newPosition.x = Mathf.Lerp(arrow.transform.position.x, initArrowPosition.x + 0.5f, Time.deltaTime * 5);
                arrow.transform.position = newPosition;
                if (arrow.transform.position.x >= initArrowPosition.x + 0.4f)
                {
                    isRight = false;
                }
            }
            else
            {
                Vector3 newPosition = arrow.transform.position;
                newPosition.x = Mathf.Lerp(arrow.transform.position.x, initArrowPosition.x - 0.5f, Time.deltaTime * 5);
                arrow.transform.position = newPosition;
                if (arrow.transform.position.x <= initArrowPosition.x - 0.4f)
                {
                    isRight = true;
                }
            }
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    public void SetIsFriend(bool state)
    {
        isFriend = state;
    }

    public void SetIce(GameObject item)
    {
        ice = item;
    }

    public void SetGlass(GameObject _glass)
    {
        glass = _glass;
    }

}

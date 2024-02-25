using Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class TutorialManager : MonoBehaviour
{
    [Header("DragScripts")]
    [SerializeField] private List<DragItems> drag;
    private List<int> initOrderingLayerDrag;
    [SerializeField] private SpriteRenderer glass;

    [Header("Rotate")]
    [SerializeField] private RotateBottle jigger;

    [Header("Shaker")]
    [SerializeField] private ShakerStateMachine shaker;
    [SerializeField] private SpriteRenderer shakerSpiteRenderer;
    [SerializeField] private LiquidManager shakerLiquid; 

    [Header("CreateObject")]
    [SerializeField] private List<BoxCollider2D> createObjectCollider;
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

    private bool[] continuConversation;

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
                initOrderingLayerDrag.Add(glass.sortingOrder);
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
      
        panel.SetActive(false);
        startTutorial = false;
        continuConversation = new bool[10];
        for(int i = 0; i<continuConversation.Length; i++)
        {
            continuConversation[i] = true;
        }

        isFriend = false;
    }

    private void Update()
    {
        ActivateTutorial();
    }

    private void ActivateTutorial()
    {
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

        if (playerConversant.GetCanContinue() && isFriend)
        {
            startTutorial = true;
        }

        if (id == tutorialID.BaseTutorial && !playerConversant.GetCanContinue() && startTutorial)
        {
            CallActiveDrag();
        }
        else if (id == tutorialID.MinigameTutorial)
        {
            CallActiveCreateObject();
        }
    }

    private void CallActiveCreateObject()
    {
        if (!fridge.GetIsOpen() && createObjectCollider[1].gameObject.GetComponent<CreateObject>().GetIsCreated())
        {
            ActiveDragItem(7, false);
        }
        else if (fridge.GetIsOpen() && drag[6].GetWasOnTheTable())
        {
            ActiveCreateObjectFridge(1);
        }
        else if (createObjectCollider[0].gameObject.GetComponent<CreateObject>().GetIsCreated() && !fridge.GetIsOpen())
        {
            ActiveDragItem(6, false);
        }
        else if (fridge.GetIsOpen())
        {
            ActiveCreateObjectFridge(0);
        }
        else
        {
            ActiveFridge();
        }
    }

    private void CallActiveDrag()
    {
        if (drag[4].GetWasOnTheTable() && !continuConversation[8])
        {
            ActiveDragItem(5, false);
            if (drag[5].GetWasOnTheTable() && continuConversation[9])
            {
                ContinueConversation();
                continuConversation[9] = false;
            }
        }
        else if (drag[3].GetWasOnTheTable() && !continuConversation[6])
        {           
            if(drag[4].GetWasOnTheTable() && continuConversation[7])
            {
                ContinueConversation();
                continuConversation[7] = false;
            }
            else if(shakerLiquid.GetDrinkState() == CocktailNode.State.Mixed && continuConversation[8])
            {
                ContinueConversation();
                continuConversation[8] = false;
            }
            else 
            {
                ActiveDragItem(4, false);
            }
        }
        else if (drag[2].GetWasOnTheTable() && !continuConversation[4])
        {
            ActiveDragItem(3, false);
            if (drag[3].GetWasOnTheTable() && !drag[3].GetIsDraggin() && continuConversation[5])
            {
                ContinueConversation();
                continuConversation[5] = false;
            }

            if(shakerLiquid.GetCurrentLiquid() >= shakerLiquid.GetMaxLiquid() && continuConversation[6])
            {
                ContinueConversation();
                continuConversation[6] = false;
            }
        }
        else if (drag[1].GetWasOnTheTable() && !continuConversation[3])
        {
            ActiveDragItem(2, false);
            if (drag[2].GetWasOnTheTable() && !drag[2].GetIsDraggin() && continuConversation[4])
            {
                ContinueConversation();
                continuConversation[4] = false;
            }
        }
        else if (shaker.GetWasInTable() && !continuConversation[2])
        {
            ActiveDragItem(1, false);
            jigger.enabled = true;
            if (drag[1].GetWasOnTheTable() && !drag[1].GetIsDraggin() && continuConversation[3])
            {
                ContinueConversation();
                continuConversation[3] = false;
            }
        }
        else if (!drag[0].GetIsDraggin() && drag[0].GetWasOnTheTable() && !drag[0].GetInsideWorkspace())
        {
            if (!shaker.GetWasInTable() && continuConversation[1])
            {
                ContinueConversation();
                continuConversation[1] = false;
            }
            else if(shaker.GetWasInTable() && (shaker.GetCurrentState().StateKey == ShakerStateMachine.ShakerState.IdleClosed ||
            shaker.GetCurrentState().StateKey == ShakerStateMachine.ShakerState.IdleOpen) && continuConversation[2])
            {
                ContinueConversation();
                continuConversation[2] = false;
            }
            else
            {
                ActiveDragShaker();
            }
            
        }
        else
        {
            ActiveDragItem(0, false);
            if (drag[0].GetWasOnTheTable() && continuConversation[0])
            {
                ContinueConversation();
                continuConversation[0] = false;
            }
        } 
    }

    private void ActiveDragShaker()
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!shaker.enabled)
        {
            shaker.enabled = true;
            shaker.SetIsInTutorial(true);

            isGrowing = true;
        }
        if ((shaker.GetCurrentState().StateKey == ShakerStateMachine.ShakerState.IdleClosed ||
            shaker.GetCurrentState().StateKey == ShakerStateMachine.ShakerState.IdleOpen) &&
            !panel.activeSelf && shaker.GetWasInTable())
        {
            shaker.SetIsInTutorial(false);
        }

        if (shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingOpen && shaker.GetIsInTutorial() && !shaker.GetIsInWorkSpace()) 
        {
            panel.SetActive(true);
            shakerSpiteRenderer.sortingOrder = 11;
            LerpSacele(maxScale, minScale, shaker.gameObject);
        }

        if(shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleOpen && shaker.GetIsInTutorial())
        {
            panel.SetActive(false);
        }

    }

    private void ActiveDragItem(int _index, bool hasToReturn)
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!drag[_index].enabled)
        {
            drag[_index].enabled = true;
            drag[_index].SetIsInTutorial(true);       

            isGrowing = true;
        }
        if (!drag[_index].GetIsDraggin() && !panel.activeSelf && drag[_index].GetWasOnTheTable())
        {       
            if(hasToReturn && !drag[_index].GetInsideWorkspace())
            {
                drag[_index].SetIsInTutorial(false);
            }
            else if(!hasToReturn)
            {
                drag[_index].SetIsInTutorial(false);
            }
        }
        if (!drag[_index].GetIsDraggin() && drag[_index].GetIsInTutorial() && !drag[_index].GetInsideWorkspace())
        {
            panel.SetActive(true);
            if(drag[_index].gameObject.GetComponent<SpriteRenderer>() != null)
            {
                drag[_index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            }
            else
            {
                glass.sortingOrder = 11;
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
                glass.sortingOrder = initOrderingLayerDrag[_index];
                glass.sortingLayerName = "Default";
            }
                               
        }
    }

    private void ActiveFridge()
    {
        Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f);

        if (!fridge.enabled)
        {
            fridge.enabled = true;
            isGrowing = true;
        }

        if(!fridge.GetIsOpen())
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, fridge.gameObject);
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        if(fridge.GetIsOpen())
        {
            panel.SetActive(false);
            fridge.gameObject.transform.localScale = Vector3.one;
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerFridge;
        }
    }

    private void ActiveCreateObjectFridge(int index)
    {
        Vector3 maxScale = new Vector3(2.1f, 2.1f, 2.1f);
        Vector3 minScale = new Vector3(1.9f, 1.9f, 1.9f);

        if (!createObjectCollider[index].enabled)
        {
            createObjectCollider[index].enabled = true;
            isGrowing = true;
        }
        if (!createObjectCollider[index].gameObject.GetComponent<CreateObject>().GetIsCreated())
        {
            panel.SetActive(true);
            LerpSacele(maxScale, minScale, createObjectCollider[index].gameObject);
            createObjectCollider[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        if(createObjectCollider[index].gameObject.GetComponent<CreateObject>().GetIsCreated())
        {
            panel.SetActive(false);
            createObjectCollider[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerBucket[index];
            createObjectCollider[index].gameObject.transform.localScale = new Vector3(2,2,2);
        }
    }

    private void LerpSacele(Vector3 maxScale, Vector3 minScale, GameObject _object)
    {
        if(isGrowing)
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
        if (!playerConversant.GetCanContinue())
        {
            //playerConversant.SetCanContinue(true);
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
        }
    }

    public void SetIsFriend(bool state)
    {
        isFriend = state;
    }


}

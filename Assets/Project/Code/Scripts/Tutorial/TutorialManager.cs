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
    [SerializeField] private List<DragItemsNew> drag;
    private List<int> initOrderingLayerDrag;
    [SerializeField] private SpriteRenderer glass;

    [Header("Rotate")]
    [SerializeField] private RotateBottle jigger;

    [Header("Shaker")]
    [SerializeField] private ShakerStateMachine shaker;
    [SerializeField] private SpriteRenderer shakerSpiteRenderer;
    private int initOrderingLayerShaker;

    [Header("CreateObject")]
    [SerializeField] private List<CreateObject> createObject;
     private List<BoxCollider2D> createObjectCollider;
    private List<int> initOrderingLayerBucket;

    [Header("OpenFridge")]
    [SerializeField] private OpenFridge fridge;
    private int initOrderingLayerFridge;

    [Header("Lerp")]
    [SerializeField] private float velocity;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    private bool[] activeTurotialDrag;
    private bool[] activeTurotialCreateItem;

    private enum tutorialID
    {
        BaseTutorial,
        GlassTutorial,
        MinigameTutorial
    }

    [SerializeField] private tutorialID id;

    bool isGrowing;

    private void Awake()
    {
        initOrderingLayerDrag = new List<int>(drag.Count);
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
        shaker.enabled = false;
        initOrderingLayerShaker = shakerSpiteRenderer.sortingOrder;

        jigger.enabled = false;

        initOrderingLayerBucket = new List<int>(createObject.Count);
        createObjectCollider = new List<BoxCollider2D>(createObject.Count);
        for (int i = 0; i< createObject.Count; i++) 
        {
            createObjectCollider.Add(createObject[i].gameObject.GetComponent<BoxCollider2D>());
            createObjectCollider[i].enabled = false;
            initOrderingLayerBucket.Add(createObject[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }
        fridge.enabled = false;
        initOrderingLayerFridge = fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

        activeTurotialDrag = new bool[7];
        activeTurotialCreateItem = new bool[7];

        for (int i = 0; i < activeTurotialDrag.Length ; i++)
        {
            activeTurotialDrag[i] = false;
        }

        for (int i = 0; i < activeTurotialCreateItem.Length; i++)
        {
            activeTurotialCreateItem[i] = false;
        }
        activeTurotialCreateItem[0] = true;
        activeTurotialDrag[0] = true;
        panel.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < drag.Count; i++)
        {
            if(!drag[i].enabled)
            {
                drag[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
        if (!shaker.enabled)
        {
            shaker.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        if(id == tutorialID.BaseTutorial)
        {
            CallActiveDrag();
            ActiveBooleandsTutorial1();
        }
        else if(id == tutorialID.MinigameTutorial)
        {
            CallActiveCreateObject();
            ActiveBooleandsTutorial3();
        }      
    }

    private void CallActiveCreateObject()
    {
        if (activeTurotialCreateItem[0])
        {
            ActiveFridge(0);
        }

        if (activeTurotialCreateItem[1])
        {
            ActiveCreateObjectFridge(0, 1);
        }

        if (activeTurotialCreateItem[2])
        {
            ActiveDragItem(6, false, 2);
        }

        if (activeTurotialCreateItem[3])
        {
            ActiveCreateObjectFridge(3, 3);
        }

        if (activeTurotialCreateItem[4])
        {
            ActiveDragItem(7, false, 4);
        }
    }

    private void ActiveBooleandsTutorial3()
    {
        if (!activeTurotialCreateItem[3] && !activeTurotialCreateItem[4] && !fridge.GetIsOpen() && createObject[3].GetIsCreated())
        {
            activeTurotialCreateItem[4] = true;
        }
        if (activeTurotialCreateItem[2] && !activeTurotialCreateItem[3] && fridge.GetIsOpen() && drag[6].GetWasOnTheTable())
        {
            activeTurotialCreateItem[3] = true;
            activeTurotialCreateItem[2] = false;
        }
        if (!activeTurotialCreateItem[1] && !activeTurotialCreateItem[2] && createObject[0].GetIsCreated() && !fridge.GetIsOpen())
        {
            activeTurotialCreateItem[2] = true;
        }

        if (!activeTurotialCreateItem[0] && !activeTurotialCreateItem[1] && fridge.GetIsOpen() && !createObject[0].GetIsCreated())
        {
            activeTurotialCreateItem[1] = true;
        }
    }

    private void CallActiveDrag()
    {
        if (activeTurotialDrag[0])
        {
            ActiveDragItem(0, true, 0);
        }

        if (activeTurotialDrag[1])
        {
            ActiveDragShaker();
        }

        if (activeTurotialDrag[2])
        {
            ActiveDragItem(1, false, 2);
            jigger.enabled = true;
        }

        if (activeTurotialDrag[3])
        {
            ActiveDragItem(2, false, 3);
        }

        if (activeTurotialDrag[4])
        {
            ActiveDragItem(3, false, 4);
        }

        if (activeTurotialDrag[5])
        {
            ActiveDragItem(4, false, 5);
        }

        if (activeTurotialDrag[6])
        {
            ActiveDragItem(5, false, 6);
        }

        if (activeTurotialDrag[6])
        {
            ActiveDragItem(5, false, 6);
        }
    }

    private void ActiveBooleandsTutorial1()
    {
        if (!activeTurotialDrag[6] && !activeTurotialDrag[7] && drag[5].GetWasOnTheTable() && !createObject[0].GetIsCreated())
        {
            activeTurotialDrag[7] = true;
        }

        if (!activeTurotialDrag[5] && !activeTurotialDrag[6] && drag[4].GetWasOnTheTable())
        {
            activeTurotialDrag[6] = true;
        }

        if (!activeTurotialDrag[4] && !activeTurotialDrag[5] && drag[3].GetWasOnTheTable())
        {
            activeTurotialDrag[5] = true;
        }

        if (!activeTurotialDrag[3] && !activeTurotialDrag[4] && drag[2].GetWasOnTheTable())
        {
            activeTurotialDrag[4] = true;
        }

        if (!activeTurotialDrag[2] && !activeTurotialDrag[3] && drag[1].GetWasOnTheTable())
        {
            activeTurotialDrag[3] = true;
        }

        if (!activeTurotialDrag[1] && !activeTurotialDrag[2] && shaker.GetWasInTable())
        {
            activeTurotialDrag[2] = true;
        }

        if (drag[0].GetHasToReturn() && !activeTurotialDrag[0] && !activeTurotialDrag[1])
        {
            activeTurotialDrag[1] = true;
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
            activeTurotialDrag[1] = false;
        }

        if (shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.DraggingOpen && shaker.GetIsInTutorial() && !shaker.GetIsInWorkSpace()) 
        {
            panel.SetActive(true);
            shakerSpiteRenderer.sortingOrder = 11;
            LerpSacelShaker(maxScale, minScale);
        }

        if(shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleClosed &&
            shaker.GetCurrentState().StateKey != ShakerStateMachine.ShakerState.IdleOpen && shaker.GetIsInTutorial())
        {
            panel.SetActive(false);
        }

    }

    private void ActiveDragItem(int _index, bool hasToReturn, int _indexTutorial)
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
                activeTurotialDrag[_indexTutorial] = false;
            }
            else if(!hasToReturn)
            {
                drag[_index].SetIsInTutorial(false);
                activeTurotialDrag[_indexTutorial] = false;
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
            
            LerpSaceleDrag(_index, maxScale, minScale);
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

    private void ActiveFridge(int _indexTutorial)
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
            LerpScaleFridge(maxScale, minScale);
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        if(fridge.GetIsOpen())
        {
            panel.SetActive(false);
            fridge.gameObject.transform.localScale = Vector3.one;
            fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerFridge;
            activeTurotialCreateItem[_indexTutorial] = false;
        }
    }

    private void ActiveCreateObjectFridge(int index, int _indexTutorial)
    {
        Vector3 maxScale = new Vector3(2.1f, 2.1f, 2.1f);
        Vector3 minScale = new Vector3(1.9f, 1.9f, 1.9f);

        if (!createObjectCollider[index].enabled)
        {
            createObjectCollider[index].enabled = true;
            isGrowing = true;
        }
        if (!createObject[index].GetIsCreated())
        {
            panel.SetActive(true);
            LerpScaleCreate(index, maxScale, minScale);
            createObject[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        if(createObject[index].GetIsCreated())
        {
            panel.SetActive(false);
            createObject[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder = initOrderingLayerBucket[index];
            createObject[index].gameObject.transform.localScale = new Vector3(2,2,2);
            activeTurotialCreateItem[_indexTutorial] = false;
        }
    }

    private void LerpSaceleDrag(int _index, Vector3 maxScale, Vector3 minScale)
    {
        if(isGrowing)
        {
            drag[_index].gameObject.transform.localScale = Vector3.Lerp(drag[_index].gameObject.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (drag[_index].gameObject.transform.localScale.x >= maxScale.x - 0.01) 
            {        
                isGrowing = false;
            }
        }
        else
        {
            drag[_index].gameObject.transform.localScale = Vector3.Lerp(drag[_index].gameObject.transform.localScale, minScale, velocity * Time.deltaTime);
            if (drag[_index].gameObject.transform.localScale.x <= minScale.x + 0.01) 
            {    
                isGrowing = true;
            }
        }             
    }

    private void LerpSacelShaker(Vector3 maxScale, Vector3 minScale)
    {
        if (isGrowing)
        {
           shaker.gameObject.transform.localScale = Vector3.Lerp(shaker.gameObject.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (shaker.gameObject.transform.localScale.x >= maxScale.x - 0.01)
            {
                isGrowing = false;
            }
        }
        else
        {
            shaker.gameObject.transform.localScale = Vector3.Lerp(shaker.gameObject.transform.localScale, minScale, velocity * Time.deltaTime);
            if (shaker.gameObject.transform.localScale.x <= minScale.x + 0.01)
            {
                isGrowing = true;
            }
        }
    }

    private void LerpScaleFridge(Vector3 maxScale, Vector3 minScale)
    {
        if (isGrowing)
        {
            fridge.gameObject.transform.localScale = Vector3.Lerp(fridge.gameObject.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (fridge.gameObject.transform.localScale.x >= maxScale.x - 0.01)
            {
                isGrowing = false;
            }
        }
        else
        {
            fridge.gameObject.transform.localScale = Vector3.Lerp(fridge.gameObject.transform.localScale, minScale, velocity * Time.deltaTime);
            if (fridge.gameObject.transform.localScale.x <= minScale.x + 0.01)
            {
                isGrowing = true;
            }
        }
    }

    private void LerpScaleCreate(int index, Vector3 maxScale, Vector3 minScale)
    {
        if (isGrowing)
        {
            createObject[index].gameObject.transform.localScale = Vector3.Lerp(createObject[index].gameObject.transform.localScale, maxScale, velocity * Time.deltaTime);
            if (createObject[index].gameObject.transform.localScale.x >= maxScale.x - 0.01)
            {
                isGrowing = false;
            }
        }
        else
        {
            createObject[index].gameObject.transform.localScale = Vector3.Lerp(createObject[index].gameObject.transform.localScale, minScale, velocity * Time.deltaTime);
            if (createObject[index].gameObject.transform.localScale.x <= minScale.x + 0.01)
            {
                isGrowing = true;
            }
        }
    }
}

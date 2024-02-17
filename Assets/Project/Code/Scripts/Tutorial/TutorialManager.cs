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
    private List<int> initOrderingLayerBucket;

    [Header("OpenFridge")]
    [SerializeField] private OpenFridge fridge;
    private int initOrderingLayerFridge;

    [Header("Lerp")]
    [SerializeField] private float velocity;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    private bool[] activeTurotial;

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
        for (int i = 0; i< createObject.Count; i++) 
        {
            createObject[i].enabled = false;
            initOrderingLayerBucket.Add(createObject[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }
        fridge.enabled = false;
        initOrderingLayerFridge = fridge.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

        activeTurotial = new bool[15];

        for(int i = 0; i < activeTurotial.Length ; i++)
        {
            activeTurotial[i] = false;
        }

        activeTurotial[0] = true;
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

        CallActiveDrag();
        ActiveBooleands();
    }

    private void CallActiveDrag()
    {
        if (activeTurotial[0])
        {
            ActiveDragItem(0, true, 0);
        }

        if (activeTurotial[1])
        {
            ActiveDragShaker();
        }

        if (activeTurotial[2])
        {
            ActiveDragItem(1, false, 2);
            jigger.enabled = true;
        }

        if (activeTurotial[3])
        {
            ActiveDragItem(2, false, 3);
        }

        if (activeTurotial[4])
        {
            ActiveDragItem(3, false, 4);
        }

        if (activeTurotial[5])
        {
            ActiveDragItem(4, false, 5);
        }

        if (activeTurotial[6])
        {
            ActiveDragItem(5, false, 6);
        }

        if (activeTurotial[6])
        {
            ActiveDragItem(5, false, 6);
        }

        if (activeTurotial[7])
        {
            ActiveFridge(7);
        }
    }

    private void ActiveBooleands()
    {
        if (!activeTurotial[6] && !activeTurotial[7] && drag[5].GetWasOnTheTable())
        {
            activeTurotial[7] = true;
        }

        if (!activeTurotial[5] && !activeTurotial[6] && drag[4].GetWasOnTheTable())
        {
            activeTurotial[6] = true;
        }

        if (!activeTurotial[4] && !activeTurotial[5] && drag[3].GetWasOnTheTable())
        {
            activeTurotial[5] = true;
        }

        if (!activeTurotial[3] && !activeTurotial[4] && drag[2].GetWasOnTheTable())
        {
            activeTurotial[4] = true;
        }

        if (!activeTurotial[2] && !activeTurotial[3] && drag[1].GetWasOnTheTable())
        {
            activeTurotial[3] = true;
        }

        if (!activeTurotial[1] && !activeTurotial[2] && shaker.GetWasInTable())
        {
            activeTurotial[2] = true;
        }

        if (drag[0].GetHasToReturn() && !activeTurotial[0] && !activeTurotial[1])
        {
            activeTurotial[1] = true;
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
            activeTurotial[1] = false;
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
            shakerSpiteRenderer.sortingOrder = initOrderingLayerShaker;
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
                activeTurotial[_indexTutorial] = false;
            }
            else if(!hasToReturn)
            {
                drag[_index].SetIsInTutorial(false);
                activeTurotial[_indexTutorial] = false;
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
            }
            else
            {
                glass.sortingOrder = initOrderingLayerDrag[_index];
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
            activeTurotial[_indexTutorial] = false;
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
}

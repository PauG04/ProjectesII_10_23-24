using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;
    private float time = 0;

    [SerializeField] private DragHammer hammer;

    private void Start()
    {
        m_BoxCollider= GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        DetectClick();
        if(m_BoxCollider.enabled)
        {
            StartTimer();
        }
    }

    private void DetectClick()
    {
        if(Input.GetMouseButtonDown(1) && hammer.GetDragging()) 
        {
            m_BoxCollider.enabled = true;
        }
        else if(Input.GetMouseButtonUp(1)||!hammer.GetDragging())
        {
            m_BoxCollider.enabled = false;
        }
    }

    private void StartTimer()
    {
        time += Time.deltaTime;
        if(time >= 0.05)
        {
            m_BoxCollider.enabled = false;
            time = 0;
        }
    }

}

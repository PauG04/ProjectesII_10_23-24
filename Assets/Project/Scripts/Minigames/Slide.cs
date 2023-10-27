using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Slide : MonoBehaviour
{
    private bool startLine;
    private bool EndLine;
    [SerializeField]
    private GameObject slide;
    [SerializeField]
    private GameObject app;
    [SerializeField]
    private float moveSlider;
    [SerializeField]
    private int maxSlide;
    [SerializeField]
    private int maxSlideDone;
    [SerializeField]
    private int slideDone = 0;
    private int slideMade;
    private Vector3 startVector;

    private void Start()
    {
        startVector = new Vector3(slide.transform.position.x, slide.transform.position.y, slide.transform.position.z);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetKnifePosition();
        }
        if(!Input.GetMouseButton(0))
        {
            startLine = false;
            EndLine = false;
        }
        if(slideMade == maxSlide)
        {
            app.SetActive(false);
            slide.transform.position = startVector;
        }
        if(slideDone == maxSlideDone)
        {
            RestartSlide();
        }
    }

    private void SetKnifePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("End"))
        {
            if (startLine && !EndLine)
            {            
                slideDone++;
                startLine = false;
            }
            if (slideDone % 2 != 0)
            {
                EndLine = true;
            }         
        }
        if (collision.gameObject.CompareTag("Start"))
        {
            if (EndLine && !startLine)
            {
                slideDone++;
                EndLine = false;               
            }
            if (slideDone % 2 == 0)
            {
                startLine = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            startLine= false;
            EndLine= false;
        }
    }

    private void RestartSlide()
    {
        slide.transform.position = new Vector3(slide.transform.position.x - moveSlider, slide.transform.position.y, slide.transform.position.z);
        slideMade++;
        slideDone = 0;
    }

}


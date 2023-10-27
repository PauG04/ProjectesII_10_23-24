using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideFruit : MonoBehaviour
{
    private bool startLine;
    [SerializeField]
    private GameObject slide;
    [SerializeField]
    private GameObject app;
    [SerializeField]
    private float moveSlider;
    [SerializeField]
    private int maxSlide;
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
        if (!Input.GetMouseButton(0))
        {
            startLine = false;
        }
        if (slideMade == maxSlide)
        {
            app.SetActive(false);
            slide.transform.position = startVector;
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
        if (collision.gameObject.CompareTag("End") && startLine == true)
        {
            startLine = false;
            slide.transform.position = new Vector3(slide.transform.position.x - moveSlider, slide.transform.position.y, slide.transform.position.z);
            slideMade++;

        }
        if (collision.gameObject.CompareTag("Start"))
        {
            startLine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            startLine = false;
        }
    }
}

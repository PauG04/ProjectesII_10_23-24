using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragGlass : MonoBehaviour
{
    private CreateGlass createGlass;
    private bool isLocated;
    private Rigidbody2D rigidbody;
    private bool isDragging;
    private Vector3 startScale;
    private Transform position;
    private IsEmpty isEmpty;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        createGlass = gameObject.GetComponentInParent<CreateGlass>();
        startScale = transform.localScale;
        isLocated = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEmpty = collision.GetComponent<IsEmpty>();
        if (collision.CompareTag("Glass") && isDragging == true && isEmpty.GetIsEmpty())
        {
            transform.localScale = Vector3.Lerp(startScale, transform.localScale * 7, Time.deltaTime*20);
            isLocated= true;
            position = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Glass") && isDragging == true)
        {
            transform.localScale = startScale;
            isLocated= false;
        }
    }

    private void OnMouseDown()
    {
         isDragging = true;
         createGlass.CreateNewGlass();
         transform.SetParent(null);
         startScale = transform.localScale;     
    }

    private void OnMouseUp()
    {
        if(!isLocated)
        {
            isDragging = false;
            rigidbody.isKinematic = false;
        }      
        if(isLocated) 
        {
            transform.SetParent(position);
            transform.position = position.transform.position;
            isEmpty = GetComponentInParent<IsEmpty>();
            isDragging = false;
            isEmpty.SetIsEmpty(false);
            isEmpty.SetGlass(this.gameObject);
            this.enabled = false;        
        }

    }
}

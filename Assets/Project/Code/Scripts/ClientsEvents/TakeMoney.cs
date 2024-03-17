using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMoney : MonoBehaviour
{
    [SerializeField] private float endPosition;
    [SerializeField] private float velocity;
    [SerializeField] private float price;
    private float initPosition;
    private bool isInside = false;

    private void Awake()
    {
        initPosition = transform.localPosition.y;
    }

    private void Update()
    {
        if(isInside)
        {
            Vector3 newPosition = transform.localPosition;

            newPosition.y = Mathf.Lerp(transform.localPosition.y, endPosition, Time.deltaTime * velocity);
            transform.localPosition = newPosition;
        }
        else if(!isInside)
        {
            Vector3 newPosition = transform.localPosition;

            newPosition.y = Mathf.Lerp(transform.localPosition.y, initPosition, Time.deltaTime * velocity);
            transform.localPosition = newPosition;
        }
    }

    private void OnMouseDown()
    {
        if(transform.parent.GetComponent<DragItems>().GetInsideWorkspace())
        {
            Destroy(gameObject);
            EconomyManager.instance.AddMoney(price);
        }
    }

    private void OnMouseOver()
    {
        isInside = true;
    }

    private void OnMouseExit()
    {
        isInside = false;
    }

}

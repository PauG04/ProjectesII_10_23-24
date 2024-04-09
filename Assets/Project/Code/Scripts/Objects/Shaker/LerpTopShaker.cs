using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LerpTopShaker : MonoBehaviour
{
    private Vector3 initPosition;

    private Quaternion initRotation;

    [Header("Shaker Top")]
    [SerializeField] private SetTopShaker setTopShaker;

    private DragPhysicObject dragPhysicObject;

    [Header("Velocity Lerp")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float velocityZ;

    [Header("CreatedItem")]
    [SerializeField] private bool hasToBeDestroy;


    private void Start()
    {
        dragPhysicObject = GetComponent<DragPhysicObject>();

        initRotation = transform.rotation;
        initPosition = transform.localPosition;
    }

    private void Update()
    {
        MoveObjectToParent();
    }
    private void MoveObjectToParent()
    {
        if (!dragPhysicObject.GetMouseDown() && !dragPhysicObject.GetIsInWorkSpace() && !setTopShaker.GetIsShakerClosed())
        {
            RotateObject();

            transform.localPosition = new Vector2(
                Mathf.Lerp(transform.localPosition.x, initPosition.x, Time.deltaTime * velocityX),
                transform.localPosition.y
            );

            if (transform.localPosition.x > initPosition.x - 0.002 && transform.localPosition.x < initPosition.x + 0.002)
            {
                transform.localPosition = new Vector2(
                    transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, initPosition.y, Time.deltaTime * velocityY)
                );

                if (transform.localPosition.y > initPosition.y - 0.002 && transform.localPosition.y < initPosition.y + 0.002)
                {
                    if (hasToBeDestroy)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void RotateObject()
    {
        transform.rotation = Quaternion.Lerp(transform.localRotation, initRotation, Time.deltaTime * velocityZ);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LerpTopShaker : MonoBehaviour
{
    [Header("Parent Position")]
    [SerializeField] private GameObject parent;

    private bool firstLerp;
    private bool secondLerp;
    private bool rotateLerp;

    private Quaternion initRotation;

    [Header("Shaker Top")]
    [SerializeField] private SetTopShaker setTopShaker;

    private DragPhysicObject dragPhysicObject;

    [Header("Velocity Lerp")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float velocityZ;

    private void Start()
    {
        dragPhysicObject = GetComponent<DragPhysicObject>();

        rotateLerp = false;
        firstLerp = false;
        secondLerp = false;

        initRotation = transform.rotation;
    }

    private void Update()
    {
        MoveObjectToParent();
    }
    private void MoveObjectToParent()
    {
        if (!dragPhysicObject.GetMouseDown() && !dragPhysicObject.GetIsInWorkSpace() && !setTopShaker.GetIsShakerClosed())
        {
            if (rotateLerp)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, initRotation, Time.deltaTime * velocityZ);
            }
            if ((transform.rotation.z == initRotation.z || transform.rotation.z == -initRotation.z) && rotateLerp)
            {
                rotateLerp = false;
                firstLerp = true;
            }

            if (firstLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.x = Mathf.Lerp(transform.position.x, parent.transform.position.x, Time.deltaTime * velocityX);

                transform.position = newPosition;
            }
            if (transform.position.x > parent.transform.position.x - 0.02 && transform.position.x < parent.transform.position.x + 0.02)
            {
                firstLerp = false;
                secondLerp = true;
            }

            if (secondLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.y = Mathf.Lerp(transform.position.y, parent.transform.position.y, Time.deltaTime * velocityY);

                transform.position = newPosition;
            }
            if (transform.position.y > parent.transform.position.y - 0.02 && transform.position.y < parent.transform.position.y + 0.02)
            {
                secondLerp = false;
                dragPhysicObject.SetIsLerp(false);
            }
        }
    }

    private void OnMouseDown()
    {
        rotateLerp = false;
        firstLerp = false;
        secondLerp = false;

        dragPhysicObject.SetRotation(5.0f);
    }
    private void OnMouseUp()
    {
        if (!dragPhysicObject.GetIsInWorkSpace())
        {
            startLerp(true);
        }
    }

    public void startLerp(bool state)
    {
        rotateLerp = state;
        dragPhysicObject.SetIsLerp(state);
        dragPhysicObject.SetRotation(0.0f);
    }
}

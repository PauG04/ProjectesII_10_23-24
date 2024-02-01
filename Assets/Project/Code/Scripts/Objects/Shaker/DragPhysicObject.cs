using System;
using Unity.VisualScripting;
using UnityEngine;

public class DragPhysicObject : MonoBehaviour
{
    [SerializeField] private LayerMask dragLayers;
    [SerializeField] private float rotationSpeed = 5.0f;

    [Header("Joint Configuration")]
    [Range(0.0f, 10.0f)][SerializeField] private float damping = 1.0f;
    [Range(0.0f, 10.0f)][SerializeField] private float frequency = 5.0f;

    private Rigidbody2D rb;
    private TargetJoint2D targetJoint;
    private Vector3 worldPos;
    [SerializeField] private bool isMouseDown;

    private bool isInWorkSpace;
    private Vector2 initScale;

    [Header("WorkSpace Scale")]
    [SerializeField] private float increaseScale;

    [Header("Parent Position")]
    [SerializeField] private GameObject parent;

    private bool firstLerp;
    private bool secondLerp;
    private bool rotateLerp;
    private bool isLerping;

    private Quaternion initRotation;

    [Header("Velocity Lerp")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float velocityZ;

    [Header("Shaker Top")]
    [SerializeField] private SetTopShaker setTopShaker;

    private void Awake()
    {
        initScale= transform.localScale;
        isInWorkSpace = false;

        rotateLerp = false;
        firstLerp = false;
        secondLerp = false;
        isLerping = false;

        initRotation = transform.localRotation;

        rotationSpeed = 0.0f;
    }

    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MoveObjectToParent();

        if (targetJoint != null)
        {
            targetJoint.target = worldPos;
        }
        else
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z) > 0.01f)
            {
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void MoveObjectToParent()
    {
        if (!isMouseDown && !isInWorkSpace && !setTopShaker.GetIsShakerClosed())
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
                isLerping = false;
            }
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;

        rotationSpeed = 5.0f;
        rotateLerp = false;
        firstLerp = false;
        secondLerp = false;

        Collider2D collider = Physics2D.OverlapPoint(worldPos, dragLayers);
        if (!collider)
        {
            return;
        }

        rb = gameObject.AddComponent<Rigidbody2D>();
        if (!rb)
        {
            return;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.None;

        targetJoint = gameObject.AddComponent<TargetJoint2D>();
        targetJoint.dampingRatio = damping;
        targetJoint.frequency = frequency;

        targetJoint.anchor = targetJoint.transform.InverseTransformPoint(worldPos);
    }
    private void OnMouseUp()
    {
        rotationSpeed = 0.0f;
        isMouseDown = false;
        Destroy(targetJoint);
        targetJoint = null;
        Destroy(rb);
        rb = null;
        
        if(!isInWorkSpace)
        {
            rotateLerp = true;
            isLerping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && isInWorkSpace)
        {       
            isInWorkSpace = false;
            transform.localScale = initScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && !isInWorkSpace)
        {
            isInWorkSpace = true;
            transform.localScale *= increaseScale;
            rotateLerp = false;
            firstLerp = false;
            secondLerp = false;
            isLerping = false;
        }
    }
    public bool GetMouseDown()
    {
        return isMouseDown;
    }

    public bool GetIsLerp()
    {
        return isLerping;
    }

    public void SetIsLerp(bool state)
    {
        isLerping = state;
        rotateLerp = state;
    }
}

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

    private void Awake()
    {
        initScale= transform.localScale;
        isInWorkSpace = false;
    }

    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
    private void OnMouseDown()
    {
        isMouseDown = true;

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
        isMouseDown = false;
        Destroy(targetJoint);
        targetJoint = null;
        Destroy(rb);
        rb = null;
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
        }
    }

    public bool GetMouseDown()
    {
        return isMouseDown;
    }


}

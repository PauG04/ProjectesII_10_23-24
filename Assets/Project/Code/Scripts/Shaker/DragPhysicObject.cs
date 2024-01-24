using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragPhysicObject : MonoBehaviour
{
    [SerializeField] private LayerMask dragLayers;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float damping = 1.0f;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float frequency = 5.0f;

    [SerializeField] private float rotationSpeed = 5.0f;

    private TargetJoint2D targetJoint;
    private Vector3 worldPos;
    private Rigidbody2D rb;
    [SerializeField] private bool isMouseDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (targetJoint)
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
        transform.SetParent(null);

        Collider2D collider = Physics2D.OverlapPoint(worldPos, dragLayers);
        if (!collider)
        {
            return;
        }
        if (!rb)
        {
            return;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.None;

        targetJoint = gameObject.AddComponent<TargetJoint2D>();
        targetJoint.dampingRatio = damping;
        targetJoint.frequency = frequency;

        targetJoint.anchor = targetJoint.transform.InverseTransformPoint(worldPos);
    }
    private void OnMouseUp()
    {
        isMouseDown = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Destroy(targetJoint);
        targetJoint = null;
        return;
    }

    public bool GetMouseDown()
    {
        return isMouseDown;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemsNew : MonoBehaviour
{
    [Header("Sprite Variables")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite workspaceSprite;
    [SerializeField] private float scaleMultiplier = 1.2f;

    private PolygonCollider2D itemCollider;
    private SpriteRenderer spriteRenderer;

    [Header("isDragging Variables")]
    private Rigidbody2D rb2d;
    private Collider2D workSpace;
    private Vector3 offset;
    private bool isDragging = false;
    private bool insideWorkspace = false;

    [Header("Returning Variables")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private bool hasToBeDestroy;
    private bool isRotated;
    private bool rotationLerp;
    private bool horizontalLerp;
    private bool vertialLerp;
    private Vector3 initPosition;
    private Quaternion initRotation;
    private bool resetRotation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        workSpace = GameObject.FindGameObjectWithTag("WorkSpace").GetComponent<Collider2D>();

        if (transform.rotation != Quaternion.identity)
        {
            isRotated = true;
            initRotation = transform.localRotation;
        }

        initPosition = transform.localPosition;
    }
    private void Update()
    {
        LerpAnimations();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging)
        {
            if (resetRotation)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                resetRotation = false;
            }
            transform.position = GetMouseWorldPosition() + offset;
            ChangingSpritesOnWorkspace(mousePosition);
        }

        if (insideWorkspace)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        } 
        else
        {
            rb2d.bodyType = RigidbodyType2D.Static;
        }
    }
    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPosition();
        resetRotation = true;
        isDragging = true;
    }
    private void OnMouseUp()
    {
        rb2d.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);

        if (!insideWorkspace)
        {
            if (isRotated)
            {
                rotationLerp = true;
            }
            else
            {
                horizontalLerp = true;
            }
        }

        isDragging = false;
    }
    private void ChangingSpritesOnWorkspace(Vector3 mousePosition)
    {
        if (workSpace.OverlapPoint(mousePosition))
        {
            insideWorkspace = true;

            spriteRenderer.sortingLayerName = "WorkSpace";
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            spriteRenderer.sprite = workspaceSprite;

            itemCollider.TryUpdateShapeToAttachedSprite();

            transform.localScale = new Vector2(scaleMultiplier, scaleMultiplier);
        }
        else
        {
            insideWorkspace = false;

            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            spriteRenderer.sprite = normalSprite;

            itemCollider.TryUpdateShapeToAttachedSprite();

            transform.localScale = Vector3.one;
        }
    }
    private void LerpAnimations()
    {
        if (!isDragging)
        {
            if (rotationLerp)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * rotationSpeed);
            }
            if (transform.localRotation.z >= initRotation.z - 0.01 && rotationLerp)
            {
                transform.localRotation = Quaternion.identity;
                rotationLerp = false;
                horizontalLerp = true;
            }
            if (horizontalLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.x = Mathf.Lerp(transform.localPosition.x, initPosition.x, Time.deltaTime * horizontalSpeed);

                transform.localPosition = newPosition;
            }
            if (transform.localPosition.x > initPosition.x - 0.002 && transform.localPosition.x < initPosition.x + 0.002)
            {
                horizontalLerp = false;
                vertialLerp = true;
            }

            if (vertialLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.y = Mathf.Lerp(transform.localPosition.y, initPosition.y, Time.deltaTime * verticalSpeed);

                transform.localPosition = newPosition;
            }
            if (transform.localPosition.y > initPosition.y - 0.002 && transform.localPosition.y < initPosition.y + 0.002)
            {
                vertialLerp = false;
                if (hasToBeDestroy)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public bool GetInsideWorkspace()
    {
        return insideWorkspace;
    }

    public bool GetIsDraggin()
    {
        return isDragging;
    }
}

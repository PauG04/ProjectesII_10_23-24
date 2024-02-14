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

    [Header("Dragging Variables")]
    private Rigidbody2D rb2d;
    private Collider2D workSpace;
    private Vector3 offset;
    private bool isDragging;
    private bool insideWorkspace;

    [Header("ChanegLayer")]
    [SerializeField] private bool hasToStayTheSameLayer;

    [Header("Returning Variables")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [Space(5)]
    [SerializeField] private bool hasToBeDestroy;
    private bool isObjectRotated;
    private bool isRotating;

    private Vector3 initPosition;
    private Quaternion initRotation;

    private RotateBottle rotateBottle;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rotateBottle = GetComponent<RotateBottle>();

        workSpace = GameObject.FindGameObjectWithTag("WorkSpace").GetComponent<BoxCollider2D>();

        rb2d.bodyType = RigidbodyType2D.Static;

        if (transform.rotation != Quaternion.identity)
        {
            isObjectRotated = true;
            initRotation = transform.localRotation;
        }
        else
        {
            initRotation = Quaternion.identity;
        }

        initPosition = transform.localPosition;
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            rb2d.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);

            isDragging = false;
        }

        RepositionObject();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
            isRotating = true;

            if (workSpace.OverlapPoint(mousePosition))
            {
                InsideWorkspace();
            } 
            else
            {
                OutsideWorkspace();
                transform.position = new Vector2(mousePosition.x, mousePosition.y);
            }
        }
        if (!workSpace.OverlapPoint(transform.position) && !isDragging)
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            OutsideWorkspace();
        }
        else if (!isDragging)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }

        if (isRotating)
        {
            if (rotateBottle != null)
            {
                if(!rotateBottle.GetIsRotating())
                {
                    RotateObject();
                }
            }
            else
            {
                RotateObject();
            }
        }
    }
    private void OnMouseDown()
    {
        Physics2D.IgnoreCollision(workSpace, GetComponent<PolygonCollider2D>());
        RotateObject();

        rb2d.bodyType = RigidbodyType2D.Static;

        offset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }
    private void OnMouseUp()
    {
        rb2d.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);

        isDragging = false;
    }
    private void InsideWorkspace()
    {
        insideWorkspace = true;

        if (!hasToStayTheSameLayer)
        {
            gameObject.layer = LayerMask.NameToLayer("WorkspaceObject");
        }

        
        spriteRenderer.sortingLayerName = "WorkSpace";
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        spriteRenderer.sprite = workspaceSprite;

        InsideWorkspaceRenderersChilds(transform);

        itemCollider.TryUpdateShapeToAttachedSprite();

        transform.localScale = new Vector2(scaleMultiplier, scaleMultiplier);
    }
    private void OutsideWorkspace()
    {

        insideWorkspace = false;

        if (!hasToStayTheSameLayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
            
        spriteRenderer.sortingLayerName = "Default";
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        spriteRenderer.sprite = normalSprite;

        OutsidewWorkspaceRenderersChilds(transform);

        itemCollider.TryUpdateShapeToAttachedSprite();

        transform.localScale = Vector3.one;
    }

    private void InsideWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sortingLayerName = "WorkSpace";
                renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            InsideWorkspaceRenderersChilds(child);
        }
    }
    private void OutsidewWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sortingLayerName = "Default";
                renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }

            OutsidewWorkspaceRenderersChilds(child);
        }
    }
    private void RotateObject()
    {
        if (isDragging || !isObjectRotated)
        {
            isRotating = !(transform.rotation == Quaternion.identity);

            transform.rotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.identity,
                Time.deltaTime * rotationSpeed
            );
        }

        if (isObjectRotated && !insideWorkspace)
        {
            isRotating = !(transform.rotation == initRotation);

            transform.rotation = Quaternion.Lerp(
                transform.localRotation,
                initRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
    private void RepositionObject()
    {
        if (!isDragging && !insideWorkspace)
        {
            transform.localPosition = new Vector2(
                Mathf.Lerp(transform.localPosition.x, initPosition.x, Time.deltaTime * horizontalSpeed), 
                transform.localPosition.y
            );

            if (transform.localPosition.x > initPosition.x - 0.002 && transform.localPosition.x < initPosition.x + 0.002)
            {
                transform.localPosition = new Vector2(
                    transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, initPosition.y, Time.deltaTime * verticalSpeed)
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

    public void SetIsDragging(bool state)
    {
        isDragging = state;
    }

    public void SetInitPosition(Vector3 _position)
    {
        initPosition = _position;
    }

}

using System;
using UnityEngine;

public class DragItems : MonoBehaviour
{
    [Header("Sprite Variables")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite workspaceSprite;
    [SerializeField] private float scaleMultiplier = 1.2f;

    [SerializeField] private PolygonCollider2D itemCollider;

    [Header("Dragging Variables")]
    private Transform target;
    private Rigidbody2D rb2d;
    private Collider2D workSpace;
    private Vector2 offset;
    private bool isDragging;
    private bool insideWorkspace;

    [Header("Returning Variables")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [Header("Boolean Variables")]
    [SerializeField] private bool hasToBeDestroy;
    [SerializeField] private bool hasToReturn;
    [SerializeField] private bool hasToStayTheSameLayer;
    [SerializeField] private bool changeSpriteMask;
    [SerializeField] private bool moveParent;
    [SerializeField] private bool dragWithWorkspaceSprite;
    [SerializeField] private bool isItem;
    [SerializeField] private bool isItemGroup;
    [SerializeField] private bool isPainting;

    private bool isObjectRotated;
    private bool isRotating;
    private bool isReturning;
    private bool isInTutorial;
    private bool wasOnTheTable;
    private bool hasToChangeSize;

    private Vector2 initPosition;
    private Quaternion initRotation;
    private Vector3 initScale;

    private RotateBottle rotateBottle;

    private void Awake()
    {
        target = moveParent ? transform.parent : transform;

        rb2d = target.GetComponent<Rigidbody2D>();
        rotateBottle = GetComponent<RotateBottle>();
        workSpace = GameObject.FindGameObjectWithTag("WorkSpace").GetComponent<BoxCollider2D>();

        if (itemCollider == null)
        {
            itemCollider = GetComponent<PolygonCollider2D>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (!isItem && hasToReturn && !isPainting)
        {
            rb2d.bodyType = RigidbodyType2D.Static;
        }

        if (target.rotation != Quaternion.identity)
        {
            isObjectRotated = true;
            initRotation = target.localRotation;
        }
        else
        {
            initRotation = Quaternion.identity;
        }
        initScale = transform.localScale;
        initPosition = target.localPosition;

        isInTutorial = false;
        wasOnTheTable = false;
        hasToChangeSize = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (hasToReturn && !isDragging)
        {
            RepositionObject();
        }

        DraggingParent();
        Dragging();
        if (isRotating)
        {
            if (rotateBottle != null)
            {
                if (!rotateBottle.GetIsRotating())
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
        ObjectPressed();
        if (dragWithWorkspaceSprite)
        {
            spriteRenderer.sprite = workspaceSprite;
        }
    }
    private void OnMouseUp()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        
        if(isPainting && insideWorkspace)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }

        isDragging = false;
        if (dragWithWorkspaceSprite)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
    private void Dragging()
    {
        if (isDragging)
        {
            target.position = new Vector2(GetMouseWorldPosition().x + offset.x, GetMouseWorldPosition().y + offset.y);
            isRotating = true;

            if (workSpace.OverlapPoint(GetMouseWorldPosition()))
            {
                InsideWorkspace();
            }
            else
            {
                OutsideWorkspace();
                if (changeSpriteMask)
                {
                    target.position = GetMouseWorldPosition();
                }
            }
        }
        else
        {
            if (!workSpace.OverlapPoint(target.position))
            {
                if (hasToReturn)
                {
                    rb2d.bodyType = RigidbodyType2D.Static;
                }

                OutsideWorkspace();
            }
            else
            {
                if (!isReturning)
                {
                    if(!isPainting)
                    {
                        rb2d.bodyType = RigidbodyType2D.Dynamic;
                    }
                    InsideWorkspace();
                }
                else
                {
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                    }
                }
            }
        }
    }
    private void DraggingParent()
    {
        if (moveParent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if (hit.collider != null)
                {
                    if (hit.collider.transform.IsChildOf(transform))
                    if (hit.collider.transform.IsChildOf(transform))
                    {
                        if (rb2d != null)
                        {
                            ObjectPressed();
                        }
                    }
                }
            }
        }
    }
    public void ObjectPressed()
    {
        if (target == null)
        {
            target = transform;
        }
        if (rb2d == null)
        {
            rb2d = gameObject.GetComponent<Rigidbody2D>();
        }

        RotateObject();

        rb2d.bodyType = RigidbodyType2D.Static;

        offset = (Vector2)target.position - GetMouseWorldPosition();
        isDragging = true;
        isReturning = false;
    }
    private void InsideWorkspace()
    {
        insideWorkspace = true;

        if (!hasToStayTheSameLayer)
        {
            gameObject.layer = LayerMask.NameToLayer("WorkspaceObject");
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "WorkSpace";
            if (changeSpriteMask)
            {
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            spriteRenderer.sprite = workspaceSprite;
            itemCollider.TryUpdateShapeToAttachedSprite(spriteRenderer);
        }

        InsideWorkspaceRenderersChilds(target);
        if (hasToChangeSize)
        {
            target.localScale = new Vector3(scaleMultiplier, scaleMultiplier, target.localScale.z);
        }
        if(!wasOnTheTable)
        {
            wasOnTheTable = true;
        }
    }
    private void OutsideWorkspace()
    {
        insideWorkspace = false;

        if (!hasToStayTheSameLayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
            
        if (spriteRenderer!= null)
        {
            spriteRenderer.sortingLayerName = "Default";
            if (changeSpriteMask)
            {
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            if (!dragWithWorkspaceSprite || !isDragging)
            {
                spriteRenderer.sprite = normalSprite;
            }
            itemCollider.TryUpdateShapeToAttachedSprite(spriteRenderer);
        }
     
        OutsidewWorkspaceRenderersChilds(target);

        if(!isInTutorial && hasToChangeSize)
        {
            target.localScale = initScale;
        }
        
    }
    private void InsideWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sortingLayerName = "WorkSpace";
                if (changeSpriteMask)
                {
                    renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
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
                if (changeSpriteMask)
                {
                    renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                }
            }

            OutsidewWorkspaceRenderersChilds(child);
        }
    }
    private void RotateObject()
    {
        if (!isObjectRotated || isDragging && target != null)
        {
            isRotating = !(target.rotation == Quaternion.identity);

            target.rotation = Quaternion.Lerp(
                target.localRotation,
                Quaternion.identity,
                Time.deltaTime * rotationSpeed
            );
        }

        if (isObjectRotated && !insideWorkspace && hasToReturn)
        {
            isRotating = !(target.rotation == initRotation);

            target.rotation = Quaternion.Lerp(
                target.localRotation,
                initRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
    private void RepositionObject()
    {
        if (!isDragging && !insideWorkspace)
        {
            RotateObject();
            isReturning = true;

            target.localPosition = new Vector2(
                Mathf.Lerp(target.localPosition.x, initPosition.x, Time.deltaTime * horizontalSpeed),
                target.localPosition.y
            );

            if (!isInTutorial)
                itemCollider.enabled = false;

            if (target.localPosition.x > initPosition.x - 0.002 && target.localPosition.x < initPosition.x + 0.002)
            {
                target.localPosition = new Vector2(
                    target.localPosition.x,
                    Mathf.Lerp(target.localPosition.y, initPosition.y, Time.deltaTime * verticalSpeed)
                );

                if (target.localPosition.y > initPosition.y - 0.002 && target.localPosition.y < initPosition.y + 0.002)
                {
                    if (!isInTutorial)
                        itemCollider.enabled = true;

                    if (hasToBeDestroy)
                    {
                        Destroy(gameObject);
                        if (isItem)
                        {
                            InventoryManager.instance.AddItem(gameObject.GetComponent<SetItemInGlass>().GetItemNode());
                        }
                        if (isItemGroup)
                        {
                            if (!GetComponent<BreakIce>().GetBroken())
                            {
                                InventoryManager.instance.AddItem(gameObject.GetComponent<GetItemInformation>().GetItemGroupNode());
                            }
                        }
                    }
                    
                }
            }
        }
    }
    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
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
    public bool GetIsLerping()
    {
        return isReturning;
    }
    public bool GetHasToReturn()
    {
        return hasToReturn;
    }

    public bool GetIsInTutorial()
    {
        return isInTutorial;
    }
    public bool GetWasOnTheTable()
    {
        return wasOnTheTable;
    }
    public float GetScaleMultiplier()
    {
        return scaleMultiplier;
    }
    public Vector3 GetInitScale()
    {
        return initScale;
    }
    public void SetIsDragging(bool state)
    {
        isDragging = state;
    }
    public void SetInitPosition(Vector3 _position)
    {
        initPosition = _position;
    }
    public void SetBodyGravity(float gravity)
    {
        rb2d.gravityScale = gravity;
    }
    public void SetHasToReturn(bool hasToReturn)
    {
        this.hasToReturn = hasToReturn;
    }

    public void SetItemCollider(PolygonCollider2D collider)
    {
        itemCollider = collider;
    }
    public void SetIsInWorkSpace(bool state)
    {
        insideWorkspace = state;
    }
    public void SetHasToChangeSize(bool hasToChangeSize)
    {
        this.hasToChangeSize = hasToChangeSize;
    }
    public void SetChangeSpriteMask(bool changeSpriteMask)
    {
        if (!changeSpriteMask)
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        }
        this.changeSpriteMask = changeSpriteMask;
    }
    public void SetScaleMultiplier(float scaleMultiplier)
    {
        this.scaleMultiplier = scaleMultiplier;
    }
    public void SetIsInTutorial(bool state)
    {
        isInTutorial = state;
    }   

    public void SetWorkspaceSprite(Sprite sprite)
    {
        workspaceSprite = sprite;
    }

    public Vector3 GetInitPosition()
    {
        return initPosition;
    }
}

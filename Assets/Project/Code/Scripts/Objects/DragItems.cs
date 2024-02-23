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
    private Vector3 offset;
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
    [SerializeField] private bool isItem;
    
    private bool isObjectRotated;
    private bool isRotating;
    private bool isReturning;
    private bool isInTutorial;
    private bool wasOnTheTable;

    private Vector3 initPosition;
    private Quaternion initRotation;

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
        if (spriteRenderer == null )
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (!isItem)
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
        initPosition = target.localPosition;

        isInTutorial = false;
        wasOnTheTable = false;
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (hasToReturn)
        {
            RepositionObject();
        }
        DraggingParent();
        Dragging();
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
        ObjectPressed();
    }
    private void OnMouseUp()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        isDragging = false;
    }
    private void Dragging()
    {
        if (isDragging)
        {
            target.position = GetMouseWorldPosition() + offset;
            isRotating = true;

            if (workSpace.OverlapPoint(GetMouseWorldPosition()))
            {
                InsideWorkspace();
            }
            else
            {
                OutsideWorkspace();
                target.position = new Vector2(GetMouseWorldPosition().x, GetMouseWorldPosition().y);
            }
        }
        else
        {
            if (!workSpace.OverlapPoint(target.position))
            {
                rb2d.bodyType = RigidbodyType2D.Static;
                OutsideWorkspace();
            }
            else
            {
                if (!isReturning)
                {
                    rb2d.bodyType = RigidbodyType2D.Dynamic;
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
                    {
                        Debug.Log("Child object clicked!");

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
        RotateObject();

        rb2d.bodyType = RigidbodyType2D.Static;

        offset = target.position - GetMouseWorldPosition();
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

        if(spriteRenderer != null) 
        {
            spriteRenderer.sortingLayerName = "WorkSpace";
            if (changeSpriteMask)
            {
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            spriteRenderer.sprite = workspaceSprite;
            itemCollider.TryUpdateShapeToAttachedSprite();
        }
        
        InsideWorkspaceRenderersChilds(target);

        target.localScale = new Vector2(scaleMultiplier, scaleMultiplier);

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
            spriteRenderer.sprite = normalSprite;
            itemCollider.TryUpdateShapeToAttachedSprite();
        }
     
        OutsidewWorkspaceRenderersChilds(target);

        if(!isInTutorial)
        {
            target.localScale = Vector3.one;
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
        if (!isObjectRotated || isDragging)
        {
            isRotating = !(target.rotation == Quaternion.identity);

            target.rotation = Quaternion.Lerp(
                target.localRotation,
                Quaternion.identity,
                Time.deltaTime * rotationSpeed
            );
        }

        if (isObjectRotated && !insideWorkspace)
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

            if (target.localPosition.x > initPosition.x - 0.002 && target.localPosition.x < initPosition.x + 0.002)
            {
                target.localPosition = new Vector2(
                    target.localPosition.x,
                    Mathf.Lerp(target.localPosition.y, initPosition.y, Time.deltaTime * verticalSpeed)
                );

                if (target.localPosition.y > initPosition.y - 0.002 && target.localPosition.y < initPosition.y + 0.002)
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
    public bool GetIsLerping()
    {
        return isReturning;
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

    public bool GetHasToReturn()
    {
        return hasToReturn;
    }

    public void SetIsInTutorial(bool state)
    {
        isInTutorial = state;
    }   
    
    public bool GetIsInTutorial()
    {
        return isInTutorial;
    }

    public bool GetWasOnTheTable()
    {
        return wasOnTheTable;
    }

}

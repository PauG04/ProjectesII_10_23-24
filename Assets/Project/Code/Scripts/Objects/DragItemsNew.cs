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
    private bool isDragging = false;
    private bool insideWorkspace = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<PolygonCollider2D>();
        workSpace = GameObject.FindGameObjectWithTag("WorkSpace").GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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
    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }
    void OnMouseUp()
    {
        isDragging = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        // Trick to make the object fall when collides with the floor of the table
        if (collision.gameObject.CompareTag("WorkSpaceFloor"))
        {
            float rotationDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
            float rotationAngle = Random.Range(10f, 30f);
            transform.Rotate(Vector3.forward, rotationDirection * rotationAngle);
        }
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
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

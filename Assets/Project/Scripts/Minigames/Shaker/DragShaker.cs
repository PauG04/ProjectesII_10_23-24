using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;
using static Drink;

public class DragShaker : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;   
    private Vector2 position;

    [SerializeField] private GetWindow window;
    [SerializeField] private float maxAngle;
    [SerializeField] private bool hasToRotate;
    [SerializeField] private CloseShaker close;

    #region Fluid Simulation Variables
    [Header("Fluid Simulation Variables")]
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxQuantityOfLiquid;
    [Space(20)]
    [SerializeField] private GameObject simulation;
    [SerializeField] private Renderer filterRenderer;

    private int quantityOfLiquid;
    private float time;
    #endregion

    private void Start()
    {
        targetJoint= GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
        simulation = GameObject.Find("Simulation");
    }

    private void Update()
    {
        CalculatePosition();
        if(hasToRotate)
        {
            rb.SetRotation(Vector2.Dot(rb.velocity.normalized, Vector2.up) * rb.velocity.sqrMagnitude * maxAngle);
        }
        if (window.GetWindows().GetCurrentState() == WindowsStateMachine.WindowState.Dragging)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void CalculatePosition()
    {
        if(dragging)
        {
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            targetJoint.target = (Vector2)window.GetWindows().transform.localPosition + position;
        }
    }
    private void DropLiquid(float spawnRateLiquid)
    {
        if (quantityOfLiquid > 0)
        {
            time += Time.deltaTime;

            if (time < 1.0f / spawnRateLiquid)
            {
                return;
            }
            // TODO: The color changes the material, but doing that changes all the other particles colors, find other method
            filterRenderer.material.SetColor("_Color", liquidColor);
            liquidParticle.GetComponent<LiquidParticle>().liquidType = drinksType;

            GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
            newParticle.transform.parent = simulation.transform;
            newParticle.transform.position = transform.position;
            time = 0.0f;
            quantityOfLiquid--;
        }
    }
    private void OnMouseDown()
    {
        if(close.GetClose())
        {
            dragging = true;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    public bool GetDragging()
    {
        return dragging;
    }

}

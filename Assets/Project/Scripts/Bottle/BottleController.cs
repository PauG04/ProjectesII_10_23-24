using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class BottleController : MonoBehaviour
{
    #region Bottle Variables
    [Header("BottleVariables")]
    [SerializeField] private LiquidManager.TypeOfDrink drinksType;
    [SerializeField] private Transform parentObject;

    private TargetJoint2D targetJoint2D;
    private Rigidbody2D rigidbody2D;
    #endregion
    #region Rotation Variables
    [Header("Rotation Variables")]
    [SerializeField] private GameObject shaker;
    [SerializeField] private float rotationVelocity = 5f;

    private Vector2 shakerPosition;
    private bool isRotating = false;
    private bool isShakerSpawned;
    #endregion
    #region Drag Variables
    private Vector3 offset;
    private bool isDragging = false;

    private Vector3 oldScale;
    private Quaternion oldRotation;
    #endregion
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
    #region Liquid Variables
    [Header("Liquid Variables")]
    [SerializeField] private Renderer fluidRenderer;
    [SerializeField] private Color liquidColor;

    [Header("Liquid Wooble Variables")]
    [SerializeField] private float maxWobble = 0.0075f;
    [SerializeField] private float wobbleSpeed = 0.5f;
    [SerializeField] private float recovery = 0.5f;

    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private float wobbleAmountToAddX;
    private Vector3 initPosition;

    private float liquidTime;
    #endregion

    private void Start()
    {
        parentObject = transform.parent;
        quantityOfLiquid = maxQuantityOfLiquid;
        fluidRenderer.material.SetColor("_Color", liquidColor);
        filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
        simulation = GameObject.Find("Simulation");
        targetJoint2D = GetComponent<TargetJoint2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
        targetJoint2D.enabled = false;
        initPosition = transform.localPosition;
    }
    private void Update()
    {
        // Temporal Scripts
        if (GameObject.Find("Shaker") != null)
        {
            shaker = GameObject.Find("Shaker");
        }

        if (isDragging)
        {
            HoldingBottle();
        }
        if(!isDragging)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = initPosition;
        }
        if (!isDragging && !isRotating)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = Vector3.zero;
        }
        SetLiquid();
        WobbleFluid();
    }
    private void OnMouseDown()
    {
        oldScale = transform.localScale;
        //oldRotation = transform.localRotation;
        rigidbody2D.isKinematic = false;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.SetParent(null);
        isDragging = true;
        targetJoint2D.enabled = true;
    }
    private void OnMouseUp()
    {
        transform.SetParent(parentObject);
        //rigidbody2D.velocity = new Vector2(0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        rigidbody2D.isKinematic = true;

        transform.localScale = oldScale;
        //transform.localRotation = oldRotation;
        

        transform.localPosition = Vector3.zero;

        isDragging = false;
        targetJoint2D.enabled = false;
    }
    private void HoldingBottle()
    {
        CalculatePosition();

        if (isRotating)
        {
            RotateObjectTowards();
        }

        if (Input.GetMouseButtonDown(1) && shaker != null)
        {
            rigidbody2D.isKinematic = true;
            targetJoint2D.enabled = false;
            isShakerSpawned = true;
            
            shakerPosition = shaker.transform.position;
            isRotating = true;
            
        }
        if (Input.GetMouseButtonUp(1) && shaker != null)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            targetJoint2D.enabled = false;
            rigidbody2D.isKinematic = false;
            isShakerSpawned = false;
            
            shakerPosition = Vector2.zero;
            isRotating = false;
            
        }
        if (transform.up.y < 0)
        {
            float spawnRateLiquid = (transform.up.y * spawnRate) / -1;
            DropLiquid(spawnRateLiquid);
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
            liquidParticle.GetComponent<LiquidParticle>().color = liquidColor;

            GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
            newParticle.transform.parent = simulation.transform;
            newParticle.transform.position = transform.position;
            time = 0.0f;
            quantityOfLiquid--;
        }
    }
    private void RotateObjectTowards()
    {
        if (isShakerSpawned)
        {
            Vector3 dir = (Vector3)shakerPosition - transform.position;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            Quaternion objectiveRotation = Quaternion.Euler(0, 0, -angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, objectiveRotation, rotationVelocity * Time.deltaTime);
        }
    }
    private void ResetRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationVelocity * Time.deltaTime);
    }
    private void CalculatePosition()
    {
        if(!targetJoint2D.enabled)
        {
            transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offset.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offset.y,
            0
           );      
        }
        else
        {
            targetJoint2D.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }     
    }
    private void WobbleFluid()
    {
        liquidTime += Time.deltaTime;
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (recovery));

        float pulse = 2 * Mathf.PI * wobbleSpeed;
        float wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * liquidTime);

        fluidRenderer.material.SetFloat("_WobbleX", wobbleAmountX);

        Vector3 velocity = (lastPosition - transform.position) / Time.deltaTime;
        Vector3 angularVelocity = transform.rotation.eulerAngles - lastRotation;

        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        lastPosition = transform.position;
        lastRotation = transform.rotation.eulerAngles;
    }
    private void SetLiquid()
    {
        //float fillAmount = minSlider + (quantityOfLiquid * (maxSlider - minSlider)) / maxQuantityOfLiquid;
        float fillAmount = (float)quantityOfLiquid / maxQuantityOfLiquid;
        fluidRenderer.material.SetFloat("_Fill", fillAmount);
    }

    public int GetLiquid()
    {
        return quantityOfLiquid;
    }

    public Color GetColor()
    {
        return liquidColor;
    }
}
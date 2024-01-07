using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragJigger : MonoBehaviour
{
    #region Bottle Variables
    [Header("BottleVariables")]
    [SerializeField] private TypeOfDrinks.TypeOfDrink drinksType;
    [SerializeField] private Transform parentObject;
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
	[SerializeField] private Color liquidColor;
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
        parentObject = transform.parent;
	    quantityOfLiquid = maxQuantityOfLiquid;
        
	    filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
        filterRenderer.material.SetColor("_Color", liquidColor);
        simulation = GameObject.Find("Simulation");
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
        // TODO: When we are not draggin anymore, the position stays.
        if (!isDragging || !isRotating)
        {
            ResetRotation();
        }
    }
    private void OnMouseDown()
    {
        oldScale = transform.localScale;
        oldRotation = transform.localRotation;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.SetParent(null);
        isDragging = true;
    }
    private void OnMouseUp()
    {
        transform.SetParent(parentObject);

        transform.localScale = oldScale;
        transform.localRotation = oldRotation;

        transform.localPosition = Vector3.zero;

        isDragging = false;
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
            isShakerSpawned = true;
            shakerPosition = shaker.transform.position;
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1) && shaker != null)
        {
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
        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offset.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offset.y,
            0
            );
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.U2D;

public class RotateTowards : MonoBehaviour
{
    #region Rotation Variables
    [Header("Rotation Variables")]
    public GameObject shaker;
    public Vector2 shakerPosition;

    private bool isShakerSpawned;
    [SerializeField] private Quaternion startedRotation;
    [SerializeField] private Vector3 startedScale;
    #endregion

    #region Drag Variables
    [Header("Drag Variables")]
    private Vector3 offset;
    private bool isDragging = false;
    #endregion
    
    #region Liquid Variables
    [Header("Liquid Variables")]
    [SerializeField] private GameObject simualtion;
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private float spawnRate;

    private float time;
    #endregion

    private void Start()
    {
        startedScale = transform.localScale;
        startedRotation = transform.rotation;
    }
    private void Update()
    {
        if (isDragging)
        {
            HoldingBottle();
        }
    }
    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.SetParent(null);
        isDragging = true;
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
    private void HoldingBottle()
    {
        CalculatePosition();
        RotateObjectTowards();

        if (Input.GetMouseButtonDown(1) && shaker != null)
        {
            isShakerSpawned = true;
            shakerPosition = shaker.transform.position;

            RotateObjectTowards();
        }
        if (Input.GetMouseButtonUp(1) && shaker != null)
        {
            isShakerSpawned = false;
            shakerPosition = Vector2.zero;
            transform.rotation = startedRotation;
        }
        if (transform.up.y < 0)
        {
            float spawnRateLiquid = (transform.up.y * spawnRate) / -1;
            DropLiquid(spawnRateLiquid);
        }
    }
    private void DropLiquid(float spawnRateLiquid)
    {
        if (simualtion.transform.childCount < 250)
        {
            time += Time.deltaTime;

            if (time < 1.0f / spawnRateLiquid)
            {
                return;
            }

            GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
            newParticle.transform.parent = simualtion.transform;
            newParticle.transform.position = transform.position;
            time = 0.0f;
        }
    }
    private void RotateObjectTowards()
    {
        if (isShakerSpawned)
        {
            transform.up = (shakerPosition - (Vector2)transform.position).normalized;
        }
        else
        {
            if (startedScale != Vector3.zero)
            {
                transform.localScale = startedScale;
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DropLiquidGlass : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private Color liquidColor;
    
    private Material texture;

    private float minRotationToPourLiquid = 70f;
    private float maxRotationToPourLiquid = 140f;

    private float _minRotationToMoveSpawner = 90f;
    private float _maxRotationToMoveSpawner = 180f;

     private float _spawnerPositionX = 0.19f;

    private float timeSinceLastPour = 0f;

    private RotateBottle rotateBottle;

    private void Awake()
    {
        texture = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        rotateBottle = GetComponent<RotateBottle>();
    }

    private void Update()
    {
        if (rotateBottle.GetIsRotating())
        {
            timeSinceLastPour += Time.deltaTime;

            if (rotateBottle.GetRotation() <= -minRotationToPourLiquid)
            {
                PourLiquid(true);
            }
            else if (rotateBottle.GetRotation() >= minRotationToPourLiquid)
            {
                PourLiquid(false);
            }
        }
    }

    private void PourLiquid(bool state)
    {
        texture.SetColor("_Color", liquidColor);

        float currentLiquid = (liquidManager.GetCurrentLiquid() * 100) / liquidManager.GetMaxLiquid();
        float currentRotation;

        if (state)
        {
            currentRotation = 100 - ((-rotateBottle.GetRotation() * 100) / maxRotationToPourLiquid);
        }
        else
        {
            currentRotation = 100 - ((-rotateBottle.GetRotation() * 100) / -maxRotationToPourLiquid);
        }

        float spawnerMovement;

        if (rotateBottle.GetRotation() < -90f)
        {        
            spawnerMovement = _spawnerPositionX + (rotateBottle.GetRotation() + _minRotationToMoveSpawner) * (-_spawnerPositionX / (-_maxRotationToMoveSpawner + _minRotationToMoveSpawner));
            spawnPoint.transform.localPosition = new Vector2(spawnerMovement, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() > 90f)
        {
            spawnerMovement = _spawnerPositionX + (rotateBottle.GetRotation() - _minRotationToMoveSpawner) * (_spawnerPositionX / (-_maxRotationToMoveSpawner + _minRotationToMoveSpawner));
            spawnPoint.transform.localPosition = new Vector2(-spawnerMovement, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() < 0f)
        {
            spawnPoint.transform.localPosition = new Vector2(_spawnerPositionX, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() > 0f)
        {
            spawnPoint.transform.localPosition = new Vector2(-_spawnerPositionX, spawnPoint.transform.localPosition.y);
        }


        float difference = Mathf.Abs(currentLiquid - currentRotation);
        float spawnSpeed = 2.8f;

        if (currentRotation <= currentLiquid && liquidManager.GetCurrentLiquid() > 0)
        {
            if (difference > 0)
            {
                spawnSpeed = 2.8f / difference;
            }

            float pouringInterval = Mathf.Lerp(0f, 1f, spawnSpeed);

            if (timeSinceLastPour >= pouringInterval)
            {
                GameObject liquid = GameObject.Instantiate(liquidPref, spawnPoint.position, Quaternion.identity);

                liquidManager.DeacreaseCurrentLiquid();

                timeSinceLastPour = 0;
            }
        }
    }
}

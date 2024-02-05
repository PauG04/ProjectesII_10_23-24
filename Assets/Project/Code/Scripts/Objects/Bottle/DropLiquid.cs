using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLiquid : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;

    private float minRotationToPourLiquid = 10f;
    private float maxRotationToPourLiquid = 140f;
    private float minRotationToMoveSpawner = 90f;
    private float maxRotationToMoveSpawner = 180f;
    
    private float timeSinceLastPour = 0f;
    private float spawnerPositionX = 0.19f;

    private RotateBottle rotateBottle;

    private void Start()
    {
        rotateBottle= GetComponent<RotateBottle>();
    }

    private void Update()
    {
        if(rotateBottle.GetIsRotating()) 
        {
            timeSinceLastPour += Time.deltaTime;

            if (rotateBottle.GetRotation() <= -minRotationToPourLiquid)
            {
                CalculateSpawnerPosition();
                PourLiquid();
            }
        }       
    }

    private void CalculateSpawnerPosition()
    {
        if (rotateBottle.GetRotation() < -90f)
        {
            float spawnerMovement = spawnerPositionX + (rotateBottle.GetRotation() + minRotationToMoveSpawner) * (0 - spawnerPositionX / (-maxRotationToMoveSpawner + minRotationToMoveSpawner));
            spawnPoint.localPosition = new Vector2(spawnerMovement, spawnPoint.localPosition.y);
        }
        else
        {
            spawnPoint.localPosition = new Vector2(spawnerPositionX, spawnPoint.localPosition.y);
        }
    }

    private void PourLiquid()
    {
        float currentLiquid = (liquidManager.GetCurrentLiquid() * 100) / liquidManager.GetMaxLiquid();
        float currentRotation = 100 - ((- rotateBottle.GetRotation() * 100) / maxRotationToPourLiquid);

        //Debug.Log("Current Liquid: " + currentLiquid + "%");
        //Debug.Log("Current Rot: " + currentRotation + "%");

        float difference = Mathf.Abs(currentLiquid - currentRotation);
        float spawnSpeed = 0.5f;

        if (currentRotation <= currentLiquid)
        {
            if (difference > 0)
            {
                spawnSpeed = 0.5f / difference;
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

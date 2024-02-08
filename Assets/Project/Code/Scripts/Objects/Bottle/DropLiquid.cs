using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLiquid : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;

    private float minRotationToPourLiquid = 70f;
    private float maxRotationToPourLiquid = 140f;
    
    private float timeSinceLastPour = 0f;

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
                PourLiquid();
            }
        }       
    }

    private void PourLiquid()
    {        
        float currentLiquid = (liquidManager.GetCurrentLiquid() * 100) / liquidManager.GetMaxLiquid();
        float currentRotation = 100 - ((- rotateBottle.GetRotation() * 100) / maxRotationToPourLiquid);

        float difference = Mathf.Abs(currentLiquid - currentRotation);
        float spawnSpeed = 2.8f;

        if (currentRotation <= currentLiquid)
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

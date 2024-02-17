using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropJiggerLiquid : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private DrinkNode.Type drinkType;

    private float timeSinceLastPour = 0f;
    private float minRotationToPourLiquid = 70f;

    private RotateBottle rotateBottle;

    private void Start()
    {
        rotateBottle = GetComponent<RotateBottle>();
    }

    private void Update()
    {
        if (rotateBottle.GetIsRotating())
        {
            timeSinceLastPour += Time.deltaTime;
            if (rotateBottle.GetRotation() >= minRotationToPourLiquid)
            {
                PourLiquid();
            }
            
        }
    }

    private void PourLiquid()
    {
        float spawnSpeed = 0.1f;

        if (liquidManager.GetCurrentLiquid() > 0)
        {
            float pouringInterval = Mathf.Lerp(0f, 1f, spawnSpeed);

            if (timeSinceLastPour >= pouringInterval)
            {
                GameObject liquid = GameObject.Instantiate(liquidPref, spawnPoint.position, Quaternion.identity);
                liquid.GetComponent<LiquidParticle>().SetDrinkType(drinkType);

                liquidManager.DeacreaseCurrentLiquid();

                timeSinceLastPour = 0;
            }
        }
    }

    public void SetDrinkType(DrinkNode.Type _drinkType)
    {
        drinkType = _drinkType;
    }
}

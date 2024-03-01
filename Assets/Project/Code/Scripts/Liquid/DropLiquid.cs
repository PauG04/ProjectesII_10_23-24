using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLiquid : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private DrinkNode drink;

    private Material texture;
    private RotateBottle rotateBottle;

    [SerializeField] private float minRotationToPourLiquid = 70f;
    [SerializeField] private float maxRotationToPourLiquid = 140f;

    private float timeSinceLastPour = 0f;

    [Header("Glass Variables")]
    [SerializeField] private bool isGlass;
    [SerializeField] private float spawnerPositionX;

    private float minRotationToMoveSpawner = 90f;
    private float maxRotationToMoveSpawner = 180f;

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
        if(rotateBottle.GetIsRotating()) 
        {
            timeSinceLastPour += Time.deltaTime;

            if (rotateBottle.GetRotation() <= -minRotationToPourLiquid )
            {
                PourLiquid(true);
            }
            else if(rotateBottle.GetRotation() >= minRotationToPourLiquid)
            {
                PourLiquid(false);
            }
        }       
    }

    private void PourLiquid(bool state)
    {

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

        if (isGlass)
        {
            MoveSpawnerPosition();
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

                if (!isGlass)
                {
                    texture.SetColor("_Color", drink.color);
                    liquid.GetComponent<LiquidParticle>().SetDrink(drink);
                }

                liquidManager.DeacreaseCurrentLiquid();

                timeSinceLastPour = 0;
            }
        }
    }
    private void MoveSpawnerPosition()
    {
        float spawnerMovement;

        if (rotateBottle.GetRotation() < -90f)
        {
            spawnerMovement = spawnerPositionX + (rotateBottle.GetRotation() + minRotationToMoveSpawner) * (-spawnerPositionX / (-maxRotationToMoveSpawner + minRotationToMoveSpawner));
            spawnPoint.transform.localPosition = new Vector2(spawnerMovement, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() > 90f)
        {
            spawnerMovement = spawnerPositionX + (rotateBottle.GetRotation() - minRotationToMoveSpawner) * (spawnerPositionX / (-maxRotationToMoveSpawner + minRotationToMoveSpawner));
            spawnPoint.transform.localPosition = new Vector2(-spawnerMovement, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() < 0f)
        {
            spawnPoint.transform.localPosition = new Vector2(spawnerPositionX, spawnPoint.transform.localPosition.y);
        }
        else if (rotateBottle.GetRotation() > 0f)
        {
            spawnPoint.transform.localPosition = new Vector2(-spawnerPositionX, spawnPoint.transform.localPosition.y);
        }

    }

    public DrinkNode GetDrink()
    {
        return drink;
    }

    
}


using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DropLiquid : MonoBehaviour
{
    [Header("Liquid Variables")]
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private DrinkNode drink;
    [SerializeField] private float spawnSpeed = 2.8f;
    private float spawnSpeedDefaultValue;

    [Header("Liquid Spawner")]
    [SerializeField] private Transform spawnPoint;
    [Space(10)]
    [SerializeField] private Transform rightSpawner;
    [SerializeField] private Transform leftSpawner;
    private bool isCascade = false;

    private Material texture;
    private RotateBottle rotateBottle;

    [Header("Liquid Rotation Variables")]
    [SerializeField] private float minRotationToPourLiquid = 70f;
    [SerializeField] private float maxRotationToPourLiquid = 120f;

    private float timeSinceLastPour = 0f;

    [Header("Glass Variables")]
    [SerializeField] private bool isTopOpen;
    [SerializeField] private float spawnerPositionX;

    private float minRotationToMoveSpawner = 90f;
    private float maxRotationToMoveSpawner = 180f;

    [SerializeField] private Collider2D liquidCollider;

    private void Start()
    {
        texture = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<MeshRenderer>().material;
        rotateBottle = GetComponent<RotateBottle>();
        liquidCollider = liquidManager.GetComponent<Collider2D>();
        spawnSpeedDefaultValue = spawnSpeed;
    }

    private void Update()
    {
        if (rotateBottle != null)
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
            else
            {
                if (liquidCollider != null)
                {
                    liquidCollider.enabled = true;
                }
            }
        }
        else
        {
            timeSinceLastPour += Time.deltaTime;

            if (transform.rotation.eulerAngles.z <= -minRotationToPourLiquid)
            {
                PourLiquid(true);
            }
            else if (transform.rotation.eulerAngles.z >= minRotationToPourLiquid)
            {
                PourLiquid(false);
            }
            else
            {
                if (AudioManager.instance.liquidSource.isPlaying)
                {
                    AudioManager.instance.StopPlayingLiquidSFX();
                }

                if (liquidCollider != null)
                {
                    liquidCollider.enabled = true;
                }
            }
        }
    }

    private void PourLiquid(bool state)
    {
        if (liquidCollider != null)
        {
            liquidCollider.enabled = false;
        }
        float currentLiquid = (liquidManager.GetCurrentLiquid() * 100) / liquidManager.GetMaxLiquid();
        float currentRotation;

        if (rotateBottle != null)
        {
            if (state)
            {
                currentRotation = 100 - ((-rotateBottle.GetRotation() * 100) / maxRotationToPourLiquid);
            }
            else
            {
                currentRotation = 100 - ((-rotateBottle.GetRotation() * 100) / -maxRotationToPourLiquid);
            }
        }
        else
        {
            if (state)
            {
                currentRotation = 100 - ((-transform.rotation.eulerAngles.z * 100) / maxRotationToPourLiquid);
            }
            else
            {
                currentRotation = 100 - ((-transform.rotation.eulerAngles.z * 100) / -maxRotationToPourLiquid);
            }
        }

        if (isTopOpen)
        {
            MoveSpawnerPosition();
        }

        spawnSpeed = spawnSpeedDefaultValue;
        if (isCascade) spawnSpeed = 0;

        float difference = Mathf.Abs(currentLiquid - currentRotation);

        if (currentRotation <= currentLiquid && liquidManager.GetCurrentLiquid() > 0)
        {
            AudioManager.instance.PlayLiquidSFX();

            if (difference > 0)
            {
                spawnSpeed /= difference;
            }

            float pouringInterval = Mathf.Lerp(0f, 1f, spawnSpeed);

            if (timeSinceLastPour >= pouringInterval)
            {
                GameObject liquid;

                if (!isCascade)
                {
                    liquid = GameObject.Instantiate(liquidPref, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Vector3 spawnerPosition = new Vector3(Random.Range(rightSpawner.position.x, leftSpawner.position.x), rightSpawner.position.y, rightSpawner.position.z);
                    liquid = GameObject.Instantiate(liquidPref, spawnerPosition, Quaternion.identity);
                }

                if (!isTopOpen)
                {
                    texture.SetColor("_Color", drink.color);
                    liquid.GetComponent<LiquidParticle>().SetDrink(drink);
                }
                else
                {
                    LiquidParticle liquidParticle = liquid.GetComponent<LiquidParticle>();
                    liquidParticle.SetCocktailState(liquidManager.GetDrinkState());

                    liquidParticle.SetDrink(liquidManager.GetParticleTypes().Keys.Last());
                }

                liquidManager.DeacreaseCurrentLiquid();

                timeSinceLastPour = 0;
            }
        }
    }
    private void MoveSpawnerPosition()
    {
        float spawnerMovement;

        isCascade = rotateBottle.GetRotation() > maxRotationToPourLiquid || rotateBottle.GetRotation() < -maxRotationToPourLiquid;

        if (!isCascade)
        {
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
    }

    public DrinkNode GetDrink()
    {
        return drink;
    }
}

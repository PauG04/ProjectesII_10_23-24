using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private float maxLiquid;
    [SerializeField] private float currentLiquid = 0;
    private Dictionary<DrinkNode.Type, int> particleTypes;

    [SerializeField] private CocktailNode.State currentState;

    [SerializeField] private bool isGlass;
    private DragItemsNew dragItems;
    private BoxCollider2D boxCollider;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0.1475f;
    [SerializeField] private float minColliderPos = -0.23f;

    [Header("Jigger")]
    [SerializeField] private DropJiggerLiquid dropLiquid;

    private void Awake()
    {
        particleTypes = new Dictionary<DrinkNode.Type, int>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (isGlass)
        {
            dragItems = GetComponentInParent<DragItemsNew>();
        }
    }

    private void Update()
    {
        ColliderController();
        if (isGlass)
        {
            if (particleTypes.Count > 0)
            {
                dragItems.SetHasToReturn(false);
            }
            else
            {
                dragItems.SetHasToReturn(true);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Liquid"))
        {
            if (currentLiquid < maxLiquid)
            {
                if(particleTypes.ContainsKey(collision.GetComponent<LiquidParticle>().GetDrinkType()))
                {
                    particleTypes[collision.GetComponent<LiquidParticle>().GetDrinkType()]++;
                    if(dropLiquid != null)
                    {
                        dropLiquid.SetDrinkType(collision.GetComponent<LiquidParticle>().GetDrinkType());
                    }
                    Debug.Log(collision.GetComponent<LiquidParticle>().GetDrinkType().ToString());
                }
                else
                {
                    particleTypes.Add(collision.GetComponent<LiquidParticle>().GetDrinkType(), 1);
                }
                Destroy(collision.gameObject);
                currentLiquid++;
            }
        }
    }
    private void ColliderController()
    {
        if (currentLiquid < maxLiquid)
        {
            float fill = currentLiquid / maxLiquid;
            float colliderPosition = minColliderPos + (fill * (maxColliderPos - minColliderPos)) / 1;
            transform.localPosition = new Vector3(transform.localPosition.x, colliderPosition, transform.localPosition.z);
        }

        if (boxCollider != null)
        {
            if (currentLiquid >= maxLiquid)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }
    public void DeacreaseCurrentLiquid()
    {
        currentLiquid--;
    }
    public void IncreaseCurrentLiquid()
    {
        currentLiquid++;
    }
    public float GetCurrentLiquid()
    {
        return currentLiquid;
    }
    public float GetMaxLiquid()
    {
        return maxLiquid;
    }

    public Dictionary<DrinkNode.Type, int> GetParticleTypes()
    {
        return particleTypes;
    }

    public void SetDrinkState(CocktailNode.State state)
    {
        currentState = state;
    }

    public CocktailNode.State GetDrinkState()
    {
        return currentState;
    }
}

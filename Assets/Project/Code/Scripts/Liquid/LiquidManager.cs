using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private float maxLiquid;
    [SerializeField] private float currentLiquid = 0;
    private Dictionary<DrinkNode.Type, int> particleTypes;

    [SerializeField] private CocktailNode.State currentState;

    [SerializeField] private bool isGlass;
    private DragItemsNew dragItems;

    [Header("Jigger")]
    [SerializeField] private DropJiggerLiquid dropLiquid;

    private void Awake()
    {
        particleTypes = new Dictionary<DrinkNode.Type, int>();

        if (isGlass)
            dragItems = GetComponentInParent<DragItemsNew>();
    }

    private void Update()
    {
        if (isGlass)
        {
            if (particleTypes.Count > 0)
                dragItems.SetHasToReturn(false);
            else
                dragItems.SetHasToReturn(true);
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

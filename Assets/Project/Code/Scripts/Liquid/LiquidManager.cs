using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private int maxLiquid;
    [SerializeField] private int currentLiquid = 0;
    private Dictionary<DrinkNode, int> particleTypes;

    [SerializeField] private CocktailNode.State currentState;

    [SerializeField] private bool isGlass;
    private DragItems dragItems;
    private BoxCollider2D boxCollider;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0;
    [SerializeField] private float minColliderPos = 0;

    [Header("Jigger")]
    [SerializeField] private DropJiggerLiquid dropLiquid;

    private void Awake()
    {
        particleTypes = new Dictionary<DrinkNode, int>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (isGlass)
        {
            dragItems = GetComponentInParent<DragItems>();
        }
    }

    private void Update()
    {
        foreach (KeyValuePair<DrinkNode, int> drink in particleTypes)
        {
            Debug.Log(drink.Key);
        }
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
                if (particleTypes.ContainsKey(collision.GetComponent<LiquidParticle>().GetDrink()))
                {
                    particleTypes[collision.GetComponent<LiquidParticle>().GetDrink()]++;
                    if (dropLiquid != null)
                    {
                        dropLiquid.SetDrinkType(collision.GetComponent<LiquidParticle>().GetDrink());
                    }
                    Debug.Log(collision.GetComponent<LiquidParticle>().GetDrink().ToString());
                }
                else
                {
                    particleTypes.Add(collision.GetComponent<LiquidParticle>().GetDrink(), 1);
                }
                Destroy(collision.gameObject);
                currentLiquid++;
                currentState = collision.GetComponent<LiquidParticle>().GetState();
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
    private void RemoveFirstMatchingInstance(Dictionary<DrinkNode, int> dictionary, int targetValue)
    {
        if (particleTypes.Count > 0)
        {
            foreach (KeyValuePair<DrinkNode, int> pair in dictionary)
            {
                if (pair.Value == targetValue)
                {
                    dictionary.Remove(pair.Key);
                    Debug.Log("Removing: " + pair.Key);
                    break;
                }
            }
        }
    }
    private void RemoveLiquidFromDictionary(Dictionary<DrinkNode, int> drinks)
    {
        if (particleTypes.Count > 0)
        {
            drinks[drinks.Last().Key]--;

            if (drinks[drinks.Last().Key] == 0)
            {
                drinks.Remove(drinks.Last().Key);
            }
        }
    }
    public void DeacreaseCurrentLiquid()
    {
        RemoveLiquidFromDictionary(particleTypes);
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

    public Dictionary<DrinkNode, int> GetParticleTypes()
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

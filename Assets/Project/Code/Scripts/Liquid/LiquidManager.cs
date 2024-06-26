using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LiquidManager : MonoBehaviour
{
    [Header("Liquid Values")]
    [SerializeField] private int maxLiquid;
    [SerializeField] private int currentLiquid = 0;
    [SerializeField] private CocktailNode.State currentState;
    [SerializeField] private SpriteRenderer glassLiquidRenderer;
    private Dictionary<DrinkNode, int> particleTypes;

    [Header("Drag Values")]
    [SerializeField] private bool isJigger;
    [SerializeField] private bool isGlass;
    [SerializeField] private bool isBottle;
    [SerializeField] private DragItems dragItems;
    private BoxCollider2D boxCollider;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0;
    [SerializeField] private float minColliderPos = 0;

    [Header("Jigger")]
    [SerializeField] private DropJiggerLiquid dropLiquid;

    [Header("Shaker")]
    [SerializeField] private ShakerStateMachine shaker;


    private void Awake()
    {
        particleTypes = new Dictionary<DrinkNode, int>();
        boxCollider = GetComponent<BoxCollider2D>();

        if ((isGlass && dragItems == null) || (isJigger && dragItems == null))
        {
            dragItems = GetComponentInParent<DragItems>();
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
        if (currentLiquid == 0 && isBottle)
        {
            if(transform.parent.gameObject.transform.parent.childCount > 1)
            {
                GameObject newBottle = transform.parent.gameObject.transform.parent.transform.GetChild(1).gameObject;

                newBottle.transform.localPosition = transform.parent.gameObject.GetComponent<DragItems>().GetInitPosition();
                newBottle.AddComponent<PolygonCollider2D>();
                newBottle.GetComponent<SpriteRenderer>().color = Color.white;
                newBottle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                newBottle.GetComponent<DragItems>().enabled = true;
                newBottle.GetComponent<DragItems>().SetItemCollider(newBottle.GetComponent<PolygonCollider2D>());
                newBottle.GetComponent<DragItems>().SetInitPosition(transform.parent.gameObject.GetComponent<DragItems>().GetInitPosition());
                newBottle.GetComponent<ArrowManager>().enabled = true;
                newBottle.transform.GetChild(3).gameObject.SetActive(true);

            }
            Destroy(transform.parent.gameObject.GetComponent<DragItems>());

            transform.parent.gameObject.transform.SetParent(null);
            transform.parent.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isBottle = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Liquid"))
        {
            if (currentLiquid < maxLiquid)
            {
                LiquidParticle particle = collision.GetComponent<LiquidParticle>();

                if (particleTypes.ContainsKey(particle.GetDrink()))
                {
                    particleTypes[particle.GetDrink()]++;
                    if (dropLiquid != null)
                    {
                        dropLiquid.SetDrinkType(particle.GetDrink());
                    }
                }
                else
                {
                    particleTypes.Add(particle.GetDrink(), 1);
                }
                if (isGlass)
                {
                    glassLiquidRenderer.color = CombineColors(particleTypes);
                }
                Destroy(collision.gameObject);

                currentLiquid++;
                currentState = particle.GetState();

                if(shaker != null)
                {
                    if (shaker.GetProgress() > 0  && particle.GetState() == CocktailNode.State.Idle)
                    {
                        shaker.SetReset(true);
                    }
                }               
            }
        }
    }
    private void ColliderController()
    {
        if (currentLiquid < maxLiquid && isGlass)
        { 
            float fill = (float)currentLiquid / (float)maxLiquid;
            float colliderPosition = (minColliderPos + (fill * (maxColliderPos - minColliderPos)) / 1);
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
    private Color CombineColors(Dictionary<DrinkNode, int> colors)
    {
        Color result = new Color(0, 0, 0, 0);
        int totalQuantityOfLiquid = 0;

        foreach (KeyValuePair<DrinkNode, int> c in colors)
        {
            Color nodeColor = c.Key.color;
            int quantity = c.Value;

            result += nodeColor * quantity;

            totalQuantityOfLiquid += quantity;
        }
        if (totalQuantityOfLiquid > 0)
        {
            result /= totalQuantityOfLiquid;
        }
        else
        {
            result = new Color(0, 0, 0, 0);
        }

        return result;
    }
    public int GetCurrentLiquid()
    {
        return currentLiquid;
    }
    public int GetMaxLiquid()
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
    public void SetCurrentLiquid(int liquid)
    {
        currentLiquid = liquid;
    }
}

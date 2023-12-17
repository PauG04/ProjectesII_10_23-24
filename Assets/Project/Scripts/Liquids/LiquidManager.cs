using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LiquidManager : MonoBehaviour
{
    #region ENUMS
    public enum TypeOfDrink
    {
        //Alcoholic Drinks
        Rum,
        Gin,
        Vodka,
        Whiskey,
        Tequila,
        //Juices
        OrangeJuice,
        LemonJuice,
        //Soft Drinks
        Cola,
        Soda,
        Tonic
    }
    
    public enum DrinkState
    {
        Idle,
        Shaked,
        Mixed
    }

    public enum DrinkCategory
    {
        Alcohol,
        Soda
    }
    #endregion

    private DrinkState drinkState;

    [Header("Renderer Variables")]
    [SerializeField] private Renderer fluidRenderer;
    [SerializeField] private int maxCapacity;
    
    private Dictionary<TypeOfDrink, int> typeOfDrinkInside;  
    private float fill;
    private int currentLayer;
    [SerializeField] private int numberOfParticles = 0;

    [Header("Shaker Variables")]
    [SerializeField] private bool isShaker;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0.1475f;
    [SerializeField] private float minColliderPos = -0.23f;

    private void Awake()
    {
        typeOfDrinkInside = new Dictionary<LiquidManager.TypeOfDrink, int>();
        currentLayer = gameObject.layer;
        drinkState = DrinkState.Idle;
    }
    private void Update()
    {
        if (isShaker)
        {
            FillShaker();
        }
        else
        {
            FillDrink();
        }
    }

    private void FillDrink()
    {
        fluidRenderer.material.SetFloat("_Fill", fill);

        if (numberOfParticles < maxCapacity)
        {
            fill = (float)numberOfParticles / maxCapacity;
            float colliderPosition = minColliderPos + (fill * (maxColliderPos - minColliderPos)) / 1;
            transform.localPosition = new Vector3(transform.localPosition.x, colliderPosition, transform.localPosition.z);
        }
    }

    private void FillShaker()
    {
        if (numberOfParticles < maxCapacity)
        {
            gameObject.layer = currentLayer;
            fill = numberOfParticles / maxCapacity;
        } 
        else
        {
            gameObject.layer = 0;
        }
    }

    public void ResetDrink()
    {
        typeOfDrinkInside.Clear();
        numberOfParticles = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid") && numberOfParticles < maxCapacity)
        {
            LiquidParticle particleCollision = collision.GetComponent<LiquidParticle>();

            if (!typeOfDrinkInside.ContainsKey(particleCollision.liquidType))
            {
                typeOfDrinkInside.Add(particleCollision.liquidType, 1);
            }
            else
            {
                typeOfDrinkInside[particleCollision.liquidType]++;
            }
            //Debug.Log(particleCollision.liquidType + " has " + typeOfDrinkInside[particleCollision.liquidType] + " particles inside.");

            Destroy(collision.gameObject);
            numberOfParticles++;
        }
    }

    public Dictionary<TypeOfDrink, int> GetTypeOfDrinkInside()
    {
        return typeOfDrinkInside;
    }

    public int GetMaxCapacity()
    {
        return maxCapacity;
    }

    public void SetDrinkState(DrinkState _drinkState)
    {
        drinkState = _drinkState;
    }
}
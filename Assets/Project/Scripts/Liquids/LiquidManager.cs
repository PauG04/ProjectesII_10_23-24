using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private Renderer fluidRenderer;
    [SerializeField] private int maxCapacity;
    
    private Dictionary<Drink.TypeOfDrink, int> typeOfDrinkInside;  
    private float fill;
    private int numberOfParticles = 0;

    [Header("Liquid Fill Variables")]
    [SerializeField] private float maxColliderPos = 0.1475f;
    [SerializeField] private float minColliderPos = -0.23f;
    private void Awake()
    {
        typeOfDrinkInside = new Dictionary<Drink.TypeOfDrink, int>();
    }
    private void Update()
    {
        FillDrink();
    }

    private void FillDrink()
    {
        fluidRenderer.material.SetFloat("_Fill", fill);

        if (numberOfParticles < maxCapacity)
        {
            fill = numberOfParticles / maxCapacity;
            float colliderPosition = minColliderPos + (fill * (maxColliderPos - minColliderPos)) / 1;
            transform.localPosition = new Vector3(transform.localPosition.x, colliderPosition, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
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

    public Dictionary<Drink.TypeOfDrink, int> GetTypeOfDrinkInside()
    {
        return typeOfDrinkInside;
    }

    public int GetMaxCapacity()
    {
        return maxCapacity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private float maxLiquid;
    [SerializeField] private float currentLiquid = 0;
    private Dictionary<DrinkNode.Type, int> particleTypes;

    private void Awake()
    {
        particleTypes = new Dictionary<DrinkNode.Type, int>();
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
}

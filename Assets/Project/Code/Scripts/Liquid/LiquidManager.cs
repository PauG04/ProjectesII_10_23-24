using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private float maxLiquid;
    private float currentLiquid;

    private void Awake()
    {
        currentLiquid = 125;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
            currentLiquid++;
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

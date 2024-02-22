using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidInside : MonoBehaviour
{
    [SerializeField] private Transform glass;
    [SerializeField] private LiquidManager liquidManager;
    private Material material;

    private void Awake()
    {
        if (glass == null)
        {
            glass = transform.parent;
        }
        material = GetComponent<SpriteRenderer>().material;
        InitState();
    }
    private void Update()
    {
        material.SetFloat("_Rotation", -glass.rotation.eulerAngles.z);
        material.SetFloat("_FillAmount", CurrentLiquid((float)liquidManager.GetCurrentLiquid()));
    }

    private float CurrentLiquid(float value)
    {
        float maxRange = 1;
        float maxLiquidCapacity = liquidManager.GetMaxLiquid();

        float currentValue = value * (maxRange / maxLiquidCapacity);

        return currentValue;
    }

    private void InitState()
    {
        material.SetFloat("_WaveHeight", 0.1f);
        material.SetFloat("_WaveSpeed", 0.1f);
        material.SetFloat("_WaveFrequency", 0.1f);
        material.SetFloat("_FillAmount", 0.0f);
    }
}

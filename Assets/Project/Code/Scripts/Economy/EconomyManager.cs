using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance { get; private set; }

    private float money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(float earnings)
    {
        money += earnings;
    }

}

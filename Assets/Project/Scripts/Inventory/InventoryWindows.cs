using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindows : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        UpdateInventory();

    }

    private void UpdateInventory()
    {
        foreach (KeyValuePair<Item, int> item in gameManager.GetInventory())
        {
            
        }
    }
}

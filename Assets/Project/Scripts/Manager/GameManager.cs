using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, float> states;
    private Dictionary<ItemObject, int> inventory;

    private void Awake()
    {
        states = new Dictionary<string, float>();
        inventory = new Dictionary<ItemObject, int>();
    }

    public Dictionary<string, float> GetStates()
    {
        return states;
    }

    public Dictionary<ItemObject, int> GetInventory()
    {
        return inventory;
    }
}

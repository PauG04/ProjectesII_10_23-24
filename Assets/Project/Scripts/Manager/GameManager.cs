using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, float> states;

    private void Awake()
    {
        states = new Dictionary<string, float>();
    }

    public Dictionary<string, float> GetStates()
    {
        return states;
    }
}

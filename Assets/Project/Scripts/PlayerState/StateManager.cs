using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance { get; private set; }

    private Dictionary<string, float> states;

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

        states = new Dictionary<string, float>();
        states.Add("Health", 100);
        states.Add("Stress", 0);
        states.Add("Fatigue", 0);
    }

    public void AddToState(string name, float value)
    {
        states[name] += value;

	    if (states[name] > 100)
            states[name] = 100;
        else if (states[name] < 0)
            states[name] = 0;
    }

    public Dictionary<string, float> GetStates()
    {
        return states;
    }
}

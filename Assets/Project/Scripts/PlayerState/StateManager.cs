using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Dictionary<string, float> states = new Dictionary<string, float>();

    [SerializeField] private GameObject healthSlider;
    [SerializeField] private GameObject stressSlider;
    [SerializeField] private GameObject fatigueSlider;

    private void Awake()
    {
        states.Add("health", 10);
        states.Add("stress", 1);
        states.Add("fatigue", 1);
    }

    public void AddToState(string name, float value)
    {
        states[name] += value;

        if (states[name] > 100)
            states[name] = 100;
        else if (states[name] < 0)
            states[name] = 0;

        if (name == "health")
            healthSlider.transform.localScale = new Vector3(states[name], 0.0f, 0.0f);
        else if (name == "stress")
            stressSlider.transform.localScale = new Vector3(states[name], 0.0f, 0.0f);
        else if (name == "fatigue")
            fatigueSlider.transform.localScale = new Vector3(states[name], 0.0f, 0.0f);
    }
}

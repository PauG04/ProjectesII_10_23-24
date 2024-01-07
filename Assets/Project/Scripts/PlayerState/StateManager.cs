using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Awake()
	{
		/*
        gameManager.GetStates().Add("health", 10);
        gameManager.GetStates().Add("stress", 1);
        gameManager.GetStates().Add("fatigue", 1);
		*/    
		}

    public void AddToState(string name, float value)
    {
        gameManager.GetStates()[name] += value;

	    if (gameManager.GetStates()[name] > 10)
		    gameManager.GetStates()[name] = 10;
        else if (gameManager.GetStates()[name] < 0)
            gameManager.GetStates()[name] = 0;
    }
}

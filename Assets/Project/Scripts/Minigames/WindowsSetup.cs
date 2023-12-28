using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsSetup : MonoBehaviour
{
	private WindowsStateMachine window;

    private void Start()
	{
		//childPositions = new List<Vector3>();
    	window = gameObject.transform.parent.GetComponent<WindowsStateMachine>();
    }

	/*
	public void SaveChildPosition()
	{
		childPositions.Clear();
		
		foreach(Transform child in transform)
		{
			childPositions.Add(child.localPosition);
		}
	}

	public void LoadChildPosition()
	{
		int i = 0;
		foreach(Transform child in transform)
		{
			child.localPosition = childPositions[i];
		}
	}
	*/
	
    public WindowsStateMachine GetWindows()
    {
        return window;
    }
}

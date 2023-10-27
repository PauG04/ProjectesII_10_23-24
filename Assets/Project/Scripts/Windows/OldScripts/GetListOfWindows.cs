using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetListOfWindows : MonoBehaviour
{
    private int numberOfWindows;
    private List<GameObject> windows;

    private void Awake()
    {
        windows = new List<GameObject>();
        numberOfWindows = transform.childCount;

        for (int i = 0; i < numberOfWindows; i++)
        {
            windows.Add(transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> GetWindowsList()
    {
        return windows;
    }

    public int GetNumberOfWindows()
    {
        return numberOfWindows;
    }
}

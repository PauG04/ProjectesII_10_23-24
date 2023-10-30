using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetListOfWindows : MonoBehaviour
{
    private List<GameObject> windows;

    private void Awake()
    {
        windows = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            windows.Add(transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> GetWindowsList()
    {
        return windows;
    }
}

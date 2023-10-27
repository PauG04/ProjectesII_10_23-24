using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ReOrderWindows : Button
{
    private GetListOfWindows listOfWindows;
    private List<GameObject> windows;
    

    private void Awake()
    {
        listOfWindows = transform.parent.GetComponent<GetListOfWindows>();
    }

    private void Start()
    {
        windows = listOfWindows.GetWindowsList();

        MoveGameObjectInZ();
    }

    public void OrderGroupLayer(GameObject pressedObject)
    {
        if (windows.Contains(pressedObject))
        {
            windows.Remove(pressedObject);
        }
        windows.Insert(0, pressedObject);

        MoveGameObjectInZ();
    }

    private void MoveGameObjectInZ()
    {
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].transform.position = new Vector3(windows[i].transform.position.x, windows[i].transform.position.y, i);
        }
    }
}

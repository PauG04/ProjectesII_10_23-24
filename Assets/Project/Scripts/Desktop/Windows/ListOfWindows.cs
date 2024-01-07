using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ListOfWindows : MonoBehaviour
{
    [SerializeField] private List<GameObject> windows;

    private void Awake()
    {
        windows = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            windows.Add(transform.GetChild(i).gameObject);
        }
    }
    
    public void MoveObjectInFront(GameObject selectedWindows)
    {
        if (windows.Contains(selectedWindows))
        {
            windows.Remove(selectedWindows);
            windows.Insert(0, selectedWindows);
        }

        for (int i = 0; i < windows.Count; i++)
        {
        	if(windows[i].GetComponent<Canvas>() != null)
        	{
        		windows[i].GetComponent<Canvas>().sortingOrder = windows.Count - i - 1;
        	}
        	
            SortingGroup sortingGroup = windows[i].GetComponent<SortingGroup>();

            if (sortingGroup != null)
            {
                sortingGroup.sortingOrder = windows.Count - i - 1;
            }

            Vector3 newPosition = windows[i].transform.position;
	        newPosition.z = i;
            windows[i].transform.position = newPosition;
        }
    }
    public void AddWindowInList(GameObject obj)
    {
        windows.Add(obj);
    }
	public void RemoveWindowInList(GameObject obj)
	{
		windows.Remove(obj);
	}
    public List<GameObject> GetWindowsList()
    {
        return windows;
    }
}

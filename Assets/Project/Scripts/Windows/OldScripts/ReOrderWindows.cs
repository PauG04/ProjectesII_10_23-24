using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Windows
{ 
    public class ReOrderWindows : MonoBehaviour
    {
        [SerializeField] private GetListOfWindows listOfWindows;
        [SerializeField] private List<GameObject> windows;

        private void Start()
        {
            listOfWindows = transform.parent.GetComponent<GetListOfWindows>();
            listOfWindows.AddWindowInList(gameObject);
            MoveObjectInZ();
        }
        private void Update()
        {
            windows = listOfWindows.GetWindowsList();
        }
        public void OrderGroupLayer(GameObject pressedObject)
        {
            if (windows.Contains(pressedObject))
            {
                windows.Remove(pressedObject);
            }
            windows.Insert(0, pressedObject);

            MoveObjectInZ();
        }
        public void RemoveObjectFromList(GameObject removeObject)
        {
            if (windows.Contains(removeObject))
            {
                windows.Remove(removeObject);
            }
        }
        private void MoveObjectInZ()
        {
            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].transform.position = new Vector3(windows[i].transform.position.x, windows[i].transform.position.y, i);
            }
        }
        
    }
}
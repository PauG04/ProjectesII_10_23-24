using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Windows 
{
    [CreateAssetMenu(fileName = "New Window", menuName = "Window", order = 0)]
    public class WindowNode : ScriptableObject
    {
        private string windowID;
        [SerializeField] private string windowName;
        [SerializeField] private string windowDescription;
        [SerializeField] private GameObject prefabChild;

        private void OnEnable()
        {
            windowID = Guid.NewGuid().ToString();
        }
        public string GetWindowID()
        {
            return windowID;
        }
        public string GetWindowName()
        {
            return windowName;
        }
        public string GetWindowDescription()
        {
            return windowDescription;
        }
        public GameObject GetPrefabChild()
        {
            return prefabChild;
        }
    }
}
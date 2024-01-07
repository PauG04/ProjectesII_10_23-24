using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Windows
{ 
    public class CreateNewWindows : MonoBehaviour
    {
        private GameObject windowGroup;
        [SerializeField] private WindowNode node;
        [SerializeField] private GameObject windowPrefab;

        private void Awake()
        {
            // Intentar pensar alguna forma de mejorarlo
            windowGroup = GameObject.Find("WindowGroup");
        }
        private void OnMouseDown()
        {
            windowPrefab.GetComponent<WindowCreation>().node = node;

            GameObject prefabWindowInstance = Instantiate(windowPrefab);
            prefabWindowInstance.transform.parent = windowGroup.transform;

            prefabWindowInstance.GetComponent<WindowCreation>().UpdateWindow();
            prefabWindowInstance.GetComponent<ReOrderWindows>().OrderGroupLayer(prefabWindowInstance);
        }
    }
}

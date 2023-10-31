using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    [SerializeField] private GameManager mainWindow;
    
    
    private void OnMouseDown()
    {
        Destroy(mainWindow);
    }
}

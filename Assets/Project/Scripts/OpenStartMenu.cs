using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStartMenu : MonoBehaviour
{

    [SerializeField] private GameObject startMenu;
    private bool isOpen;
    private bool active;
    private void Start()
    {
        startMenu.SetActive(false);
        isOpen = false;
        active = false;
    }

    private void Update()
    {
        if(active) 
        {
            if (!isOpen)
            {
                startMenu.SetActive(true);
                isOpen = true;
                active = false;
            }
            else
            {
                startMenu.SetActive(false);
                isOpen = false;
                active = false;
            }
        }
    }

    private void OnMouseDown()
    {
        active = true;
    }

}

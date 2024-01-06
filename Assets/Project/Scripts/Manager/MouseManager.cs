﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Item newItem;

    void Update()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
	    	InventoryManager.instance.AddItem(newItem);
		    AudioManager.instance.Play("click");
	    }
    }
}

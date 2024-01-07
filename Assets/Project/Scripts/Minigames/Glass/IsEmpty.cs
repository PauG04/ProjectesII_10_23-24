using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEmpty : MonoBehaviour
{
    private bool isEmpty;
    private GameObject glass;

    private void Start()
    {
        isEmpty = true;
        glass = null;
    }

    public void SetIsEmpty(bool isEmpty)
    {
        this.isEmpty = isEmpty;
    }
	
	
    public bool GetIsEmpty()
	{
		if (transform.childCount <= 0)
		{
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			return true;
		}
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
		return false;
    }

    public void SetGlass(GameObject glass)
    {
        this.glass = glass;
    }
    public GameObject GetGlass() 
    {
        return glass;
    }
}

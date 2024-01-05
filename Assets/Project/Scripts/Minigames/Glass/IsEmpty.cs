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
        return isEmpty;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShaker : MonoBehaviour
{
    [SerializeField]
    private bool close;

    private void OnMouseDown()
    {
        close = !close;
    }

    public bool GetClose()
    {
        return close;
    }
}

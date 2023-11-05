using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShaker : MonoBehaviour
{
    [SerializeField]
    private bool close;

    [SerializeField] private ShakerAnimation anim;


    private void OnMouseDown()
    {
        close = !close;
        anim.SetAnimation(!close);
    }

    public bool GetClose()
    {
        return close;
    }
}

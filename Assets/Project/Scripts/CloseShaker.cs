using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShaker : MonoBehaviour
{
    [SerializeField]
    private bool close;

    private GameObject shaker;
    private ShakerAnimation anim;

    private void Awake()
    {
        shaker = GameObject.Find("CloseShaker");
        anim = shaker.GetComponent<ShakerAnimation>();
    }

    private void OnMouseDown()
    {
        close = !close;
        anim.SetAnimation(close);
    }

    public bool GetClose()
    {
        return close;
    }
}

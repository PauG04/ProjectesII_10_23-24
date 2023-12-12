using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(bool value)
    {
        anim.SetBool("isOpen", value);
    }
}

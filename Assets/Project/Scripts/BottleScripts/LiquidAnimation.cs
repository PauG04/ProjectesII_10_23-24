using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(int ounces)
    {
        anim.SetFloat("currentOunces", ounces);
    }
}

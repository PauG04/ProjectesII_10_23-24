using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiquidAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetAnimation()
    {
        anim.SetFloat("currentOunces", 10);
    }
    public void SetAnimation(int ounces)
    {
        anim.SetFloat("currentOunces", ounces);
    }
}

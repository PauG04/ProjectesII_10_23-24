using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIce : MonoBehaviour
{
    [SerializeField] private IceBreaking ice;

    private void OnMouseDown()
    {
        ice.SetHits();
    }
}

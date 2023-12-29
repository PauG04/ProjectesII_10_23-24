using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIcePosition : MonoBehaviour
{
    [SerializeField] private Vector3 position;

    private void Update()
    {
        if(transform.localPosition != position)
        {
            transform.localPosition = position;
        }
    }
}

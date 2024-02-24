using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBottle : MonoBehaviour
{

    private float destroyTime = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}

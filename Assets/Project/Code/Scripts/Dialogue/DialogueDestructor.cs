using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDestructor : MonoBehaviour
{
    [SerializeField] private float destroyDeleay = 20f;
    void Start()
    {
        Destroy(gameObject, destroyDeleay);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSquare : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    [SerializeField] private GameObject square;

    private float time = 0;
    private float maxTime = 0.5f;

    private void Update()
    {
        if(transform.localScale == scale)
        {
            time += Time.deltaTime;
            if(time>=maxTime)
            {
                square.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFather : MonoBehaviour
{
    [SerializeField] private GameObject[] sons;

    private int maxSons;

    private void Update()
    {
        for(int i = 0; i < sons.Length; i++) 
        {
            if (sons[i] == null)
            {
                maxSons++;
            }
        }

        if(maxSons > sons.Length - 1)
        {
            Destroy(gameObject);
        }
    }
}

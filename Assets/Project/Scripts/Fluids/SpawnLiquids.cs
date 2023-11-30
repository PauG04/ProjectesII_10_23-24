using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLiquids : MonoBehaviour
{
    [SerializeField] private GameObject simualtion;
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private float spawnRate;

    private float time;

    private void Update()
    {
         if(simualtion.transform.childCount < 100)
         {
             time += Time.deltaTime;

             if(time < 1.0f / spawnRate)
             {
                 return;
             }

             GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
             newParticle.transform.position = transform.position;
             time = 0.0f;
         }
    }
}

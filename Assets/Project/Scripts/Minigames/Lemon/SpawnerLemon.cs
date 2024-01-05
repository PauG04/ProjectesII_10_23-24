using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerLemon : MonoBehaviour
{
    [SerializeField] GameObject lemon;

    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    [SerializeField] private float maxTimeSpwan;
    [SerializeField] private float minTimeSpwan;
    [SerializeField] private StartCounter counter;
    [SerializeField] private TrailRenderer trailRenderer;
    
    private Transform window;
    private int currentLemon;
    private int maxLemon = 7;
    private float time;
    private bool spawnLemon;

    private void Start()
    {
        window = window = gameObject.transform.parent.gameObject.transform.parent.GetComponent<Transform>();
        spawnLemon = true;
        currentLemon = 0;     
    }

    private void Update()
    {
        if (currentLemon <= maxLemon && spawnLemon && counter.GetStartMinigae())
        {
            Spwan();
            trailRenderer.enabled = true; 
        }

        if(!spawnLemon) 
        {
            StartTimer();
        }

        if(currentLemon > maxLemon) 
        {
            counter.SetStartMinigae(false);
            currentLemon = 0;
            trailRenderer.enabled = false;
        }
        
    }

    private void Spwan()
    {
        GameObject newLemon = Instantiate(lemon, transform);
        Vector3 spawnPos = new Vector3(Random.Range(window.position.x - 0.5f, window.position.x + 0.5f), transform.position.y , transform.position.z);
        newLemon.transform.position = spawnPos;
        newLemon.GetComponent<ThrowObject>().Throw(Random.Range(minForce, maxForce));
        currentLemon++;
        spawnLemon = false;
    }

    private void StartTimer()
    {
        time += Time.deltaTime;
        if (time >= Random.Range(minTimeSpwan, maxTimeSpwan))
        {
            time = 0;
            spawnLemon = true;
        }
    }
}

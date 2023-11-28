using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerLemon : MonoBehaviour
{
    [SerializeField] GameObject lemon;

    [SerializeField] float minForce;
    [SerializeField] float maxForce;

    float maxWitdh;

    private void Start()
    {
        float camWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
        maxWitdh = camWidth - 1.5f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spwan();
        }
    }

    private void Spwan()
    {
        GameObject newLemon = Instantiate(lemon, transform);
        Vector3 spawnPos = new Vector3(Random.Range(-maxWitdh, maxWitdh), transform.position.y , transform.position.z);
        newLemon.transform.position = spawnPos;
        newLemon.GetComponent<ThrowObject>().Throw(Random.Range(minForce, maxForce));
    }
}

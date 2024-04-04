using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBeer : MonoBehaviour
{
    [SerializeField] private DrinkNode beer;
    [SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    private Material texture;

    [SerializeField] private float spawnTime = 2.8f;
    float timeSinceLastPour = 0.0f;

    private bool isActive = false;

    private void Start()
    {
        texture = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        timeSinceLastPour += Time.deltaTime;

        if (isActive && timeSinceLastPour >= spawnTime) 
        {
            GameObject liquid = GameObject.Instantiate(liquidPref, spawnPoint.position, Quaternion.identity);
            liquid.GetComponent<LiquidParticle>().SetDrink(beer);
            float offset = Random.Range(-0.01f, 0.01f);
            liquid.transform.position = new Vector3(spawnPoint.position.x + offset, liquid.transform.position.y, liquid.transform.position.z);
            texture.SetColor("_Color", beer.color);

            timeSinceLastPour = 0.0f;
        }
        
    }

    private void OnMouseDown()
    {
        isActive = true;        
    }

    private void OnMouseUp()
    {
        isActive = false;
    }
}

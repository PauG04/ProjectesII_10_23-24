using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BreakBottle : MonoBehaviour
{
    [SerializeField] private GameObject bottleBroken;
    [SerializeField] private GameObject liquid;
    [SerializeField] private float  force;
    [SerializeField] private float  liquidForce;

    private float currentHit;
    private BottleController bottle;
    private void Start()
    {
        currentHit = 0;
        bottle = GetComponent<BottleController>();
    }
    private void Update()
    {
        if(currentHit == 3)
        {
            Slice(transform.position, transform.position);
            CreateLiquid();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            currentHit++;
        }
    }
    void Slice(Vector3 direction, Vector3 pos)
    {
        GameObject newIce = Instantiate(bottleBroken, transform);

        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));
        newIce.transform.localRotation = rotation;

        foreach (Transform slice in newIce.transform)
        {
            Rigidbody2D rbLemon = slice.GetComponent<Rigidbody2D>();
            Vector3 dir = slice.transform.position - pos;
            rbLemon.AddForceAtPosition(dir.normalized * Random.Range(-force, force), pos, ForceMode2D.Impulse);
        }
        newIce.transform.parent = null;
        newIce.transform.position = transform.position;
    }

    void CreateLiquid()
    {
        for(int i = 0; i<30; i++)
        {
            GameObject newIce = Instantiate(liquid, transform);

            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));
            newIce.transform.localRotation = rotation;
            newIce.GetComponent<LiquidParticle>().color = bottle.GetColor();

            newIce.transform.localScale = new Vector2(0.1f, 0.1f);
            Rigidbody2D rbLemon = newIce.GetComponent<Rigidbody2D>();
            Vector3 dir = newIce.transform.position - transform.position;
            rbLemon.AddForceAtPosition(transform.position * Random.Range(-liquidForce, liquidForce), transform.position, ForceMode2D.Impulse);
            
            newIce.transform.parent = null;
            newIce.transform.position = transform.position;
        }
        
    }
}

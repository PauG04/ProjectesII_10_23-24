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
    [SerializeField] private float max;
    [SerializeField] private float min;
    [SerializeField] private float difference;
    [SerializeField] private Renderer filterRenderer;
    [SerializeField] private WindowsSetup windows;

    private float currentHit;
    private float HP;
    private BottleController bottle;
    private Rigidbody2D rigidbody2D; 

    private void Start()
    {
        HP = 100;
        currentHit = 0;
        bottle = GetComponent<BottleController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
    }
    private void Update()
    {
        if(currentHit == 3 || HP <= 0)
        {
            Slice(transform.position, transform.position);
            CreateLiquid();
            Destroy(gameObject);
        }
	    /*
        if(rigidbody2D.velocity.y > max)
        {
            HP -= rigidbody2D.velocity.y / difference;
        }
        if(rigidbody2D.velocity.y < min)
        {
            HP += rigidbody2D.velocity.y / difference;
        }
	    */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer") && collision.GetComponentInParent<DragHammer>().GetIsOut())
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
            filterRenderer.material.SetColor("_Color", bottle.GetColor());

            newIce.transform.localScale = new Vector2(0.1f, 0.1f);
            newIce.GetComponent<SpriteRenderer>().tag = "IceLiquid";
            Rigidbody2D rbLemon = newIce.GetComponent<Rigidbody2D>();
            Vector3 dir = newIce.transform.position - transform.position;
            rbLemon.AddForceAtPosition(transform.position * Random.Range(-liquidForce, liquidForce), transform.position, ForceMode2D.Impulse);
            
            newIce.transform.parent = null;
            newIce.transform.position = transform.position;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingIce : MonoBehaviour
{
    [SerializeField] private float maxTimer;
    [SerializeField] private float minTimer;
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private int meltPhases;
    [SerializeField] private float massLiquid;
    [SerializeField] private float minIncreaseMass;
    [SerializeField] private float maxIncreaseMass;
    [SerializeField] private int maxLiquid;
    [SerializeField] private int minLiquid;
    [SerializeField] private float reduceScale;
    
    private float timeToMelt;
    private Vector3 startScale;
    private Vector3 currentScale;
    private Vector3 meltScale;
    private float time;
    private bool startMelting;
    private bool isWaterDropped;
    private SpriteRenderer spriteRenderer;
   

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
        currentScale = startScale / meltPhases;
        meltScale = currentScale * (meltPhases - 1);
    }

    private void Update()
    {
        IsIceMelted();
        if (!startMelting)
        {
            StartTimer();
        }
        else
        {
            MeltIce();
        }
    }

    private void StartTimer()
    {
        time += Time.deltaTime;

        if (time >= timeToMelt)
        {
            time = 0;
            startMelting = true;
        }
    }

    private void MeltIce()
    {
        if (transform.localScale.y <= meltScale.y + 0.1)
        {
            isWaterDropped = false;
            startMelting = false;
            timeToMelt = Random.Range(minTimer, maxTimer);
            meltScale -= currentScale;
        }
        else
        {
            if(!isWaterDropped)
            {
                GenerateLiquid();
                isWaterDropped = true;
            }
            transform.localScale = Vector3.Lerp(transform.localScale, meltScale, 1 * Time.deltaTime);
        }           
    }

    private void IsIceMelted()
    {
        if(transform.localScale.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GenerateLiquid()
    {
        for (int i = 0; i < Random.Range(minLiquid, maxLiquid); i++)
        {
            Vector3 newPosition = new Vector3(transform.position.x - spriteRenderer.bounds.size.x/2.5f, transform.position.y + Random.Range(-spriteRenderer.bounds.size.y/2.5f, spriteRenderer.bounds.size.y/2.5f), transform.position.z);
            GameObject newParticle = Instantiate(liquidParticle, newPosition, Quaternion.identity);
            Rigidbody2D rb = newParticle.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.0f;
            SetScale(newParticle);
            IncreaseMass(rb);
        }
        for (int i = 0; i < Random.Range(minLiquid, maxLiquid); i++)
        {
            Vector3 newPosition = new Vector3(transform.position.x + spriteRenderer.bounds.size.x / 2.5f, transform.position.y + Random.Range(-spriteRenderer.bounds.size.y / 2.5f, spriteRenderer.bounds.size.y / 2.5f), transform.position.z);
            GameObject newParticle = Instantiate(liquidParticle, newPosition, Quaternion.identity);
            Rigidbody2D rb = newParticle.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.0f;
            SetScale(newParticle);
            IncreaseMass(rb);
        }
    }

    private void SetScale(GameObject newParticle)
    {
        newParticle.transform.parent = transform.parent;
        newParticle.transform.localScale = transform.localScale / reduceScale;
    }

    private void IncreaseMass(Rigidbody2D rb)
    {
        if(rb.gravityScale < massLiquid)
        {
            rb.gravityScale += Random.Range(minIncreaseMass, maxIncreaseMass);
        }
    }
}

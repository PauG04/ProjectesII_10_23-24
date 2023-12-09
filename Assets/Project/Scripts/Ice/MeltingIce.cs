using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingIce : MonoBehaviour
{
    [SerializeField] private float maxTimer;
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private int meltPhases;
    [SerializeField] private float massLiquid;
    [SerializeField] private float increaseMass;

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

        if (time >= maxTimer)
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
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Vector3 newPosition = new Vector3(transform.position.x - spriteRenderer.bounds.size.x/2, transform.position.y, transform.position.z);
            GameObject newParticle = Instantiate(liquidParticle, newPosition, Quaternion.identity);
            Rigidbody2D rb = newParticle.GetComponent<Rigidbody2D>();
            IncreaseMass(rb);
        }
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Vector3 newPosition = new Vector3(transform.position.x + spriteRenderer.bounds.size.x / 2, transform.position.y, transform.position.z);
            GameObject newParticle = Instantiate(liquidParticle, newPosition, Quaternion.identity);
            Rigidbody2D rb = newParticle.GetComponent<Rigidbody2D>();
            IncreaseMass(rb);
        }
    }

    private void IncreaseMass(Rigidbody2D rb)
    {
        if(rb.mass < massLiquid)
        {
            rb.mass += increaseMass;
        }
    }
}

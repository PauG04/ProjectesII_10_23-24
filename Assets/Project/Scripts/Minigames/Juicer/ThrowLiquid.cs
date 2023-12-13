using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThrowLiquid : MonoBehaviour
{
    [SerializeField] private Rigidbody2D sliderRigidBody2D; 
    [SerializeField] private SliderOnPoints value; 
    [SerializeField] private GameObject liquidParticle;
    [SerializeField] private int maxLiquid;
    [SerializeField] private int diference;
    [SerializeField] private int velocityDifference;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(sliderRigidBody2D.velocity.magnitude > 0 && !value.GetIsFull() && value.GetIsOut())
        {
            CreateLiquid();
        }
    }

    private void CreateLiquid()
    {
        float totalLiquid = sliderRigidBody2D.velocity.magnitude * (maxLiquid / velocityDifference);
        for (int i = 0; i< totalLiquid; i++)
        {
            Vector3 newPosition = new Vector3(transform.position.x - spriteRenderer.bounds.size.x / 2.0f, transform.position.y - spriteRenderer.bounds.size.y / 0.5f, transform.position.z);
            GameObject newParticle = Instantiate(liquidParticle, newPosition, Quaternion.identity);
            newParticle.tag = "Fluid";
            newParticle.transform.parent = transform;
            newParticle.transform.localScale /= diference;
        }
        
    }
}

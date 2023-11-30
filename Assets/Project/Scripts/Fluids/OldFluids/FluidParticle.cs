using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidParticle : MonoBehaviour
{
    public FluidConfig config;

    [Header("Positions")]
    public Vector2 position;
    public Vector2 previousPosition;
    public Vector2 visualPosition;

    [Header("Forces")]
    public Vector2 velocity = Vector2.zero;
    public Vector2 force;

    [Header("Density")]
    public float density;
    public float densityNear;

    [Header("Pressure")]
    public float pressure;
    public float pressureNear;

    [Header("Neighoubrs")]
    public List<FluidParticle> neighbours = new List<FluidParticle>();

    [Header("Grid")]
    public int gridX;
    public int gridY;

    void Awake()
    {
        position = transform.position;
        previousPosition = position;
        visualPosition = position;

        velocity = Vector2.zero;
        force = new Vector2(0, -config.gravity);
    }

    public void UpdateState()
    {
        previousPosition = position;
        velocity += force * Time.deltaTime * config.deltaTime;
        position += velocity * Time.deltaTime * config.deltaTime;

        visualPosition = position;
        transform.position = visualPosition;

        force = new Vector2(0, -config.gravity);

        velocity = (position - previousPosition) / Time.deltaTime / config.deltaTime;

        float velocityMagnitude = velocity.magnitude;
    
        if (velocityMagnitude > config.maxVelocity)
        {
            velocity = velocity.normalized * config.maxVelocity;
        }
        density = 0.0f;
        densityNear = 0.0f;

        neighbours = new List<FluidParticle>();

        if(position.y < config.simulationGround)
        {
            if (name != "fluidParticle")
            {
                Destroy(gameObject);
            }
        }
    }

    public void CalculatePressure()
    {
        pressure = config.K * (density - config.restDensity);
        pressureNear = config.KNear * densityNear;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;

        float normalVelocity = Vector2.Dot(velocity, normal);

        if (normalVelocity > 0.0f)
        {
            return;
        }

        Vector2 velocityTangent = velocity - normal * normalVelocity;
        velocity = velocityTangent - normal * normalVelocity * config.wallDamp;
        position = collision.contacts[0].point + normal * config.wallPosition;
    }

}

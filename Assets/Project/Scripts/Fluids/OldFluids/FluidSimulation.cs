using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidSimulation : MonoBehaviour
{
    [SerializeField] private FluidConfig config;

    [SerializeField] private List<FluidParticle> particles = new List<FluidParticle>();

    [SerializeField] private GameObject particleGameObject;

    [Header("Grid Size")]
    [SerializeField] private int gridSizeX = 60;
    [SerializeField] private int gridSizeY = 30;
    [SerializeField] private List<FluidParticle>[,] grid;

    // Max and min fluid movement between screen
    [SerializeField] private float xMin; 
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    private void Start()
    {
        grid = new List<FluidParticle>[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x, y] = new List<FluidParticle>();
            }
        }
    }

    private float normalDistance;
    private Vector2 particleToNeighbor;

    public void CalculateDensity(List<FluidParticle> particles)
    {
        foreach (FluidParticle p in particles)
        {
            float density = 0.0f;
            float densityNear = 0.0f;

            for (int x = p.gridX - 1; x <= p.gridX + 1; x++)
            {
                for (int y = p.gridY - 1; y <= p.gridY + 1; y++)
                {
                    if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
                    {
                        foreach (FluidParticle n in grid[x, y])
                        {
                            float distance = Vector2.Distance(p.position, n.position);

                            if (distance < config.radius)
                            {
                                normalDistance = 1 - distance / config.radius;
                                p.density += (normalDistance * 2);
                                p.densityNear += (normalDistance * 3);
                                n.density += (normalDistance * 2);
                                n.densityNear += (normalDistance * 3);

                                if (p.name != n.name)
                                {
                                    Debug.Log(p.name + " Added " + n.name + " to neigbours.");
                                }

                                p.neighbours.Add(n);
                            }
                        }
                    }
                }
            }
            p.density += density;
            p.densityNear += densityNear;
        }
    }

    public void CreatePressure(List<FluidParticle> particles)
    {
        foreach (FluidParticle p in particles)
        {
            Vector2 pressureForce = Vector2.zero;

            foreach (FluidParticle n in p.neighbours)
            {
                particleToNeighbor = n.position - p.position;
                float distance = Vector2.Distance(p.position, n.position);

                normalDistance = 1 - distance / config.radius;
                float totalPressure = ((p.pressure + n.pressure) * normalDistance * 2) + ((p.pressureNear + n.pressureNear) * normalDistance * 3);
                Vector2 pressureVector = totalPressure * particleToNeighbor.normalized;
                n.force += pressureVector;
                pressureForce += pressureVector;
            }
            p.force -= pressureForce;
        }
    }

    public void CalculateViscocity(List<FluidParticle> particles)
    {
        foreach (FluidParticle p in particles)
        {
            foreach (FluidParticle n in p.neighbours)
            {
                particleToNeighbor = n.position - p.position;
                float distance = Vector2.Distance(p.position, n.position);

                Vector2 normalPtoN = particleToNeighbor.normalized;
                float relativeDistance = distance / config.radius;
                float velocityDifference = Vector2.Dot(p.velocity - n.velocity, normalPtoN);
                if (velocityDifference > 0)
                {
                    Vector2 viscosityForce = (1 - relativeDistance) * velocityDifference * config.viscocity * normalPtoN;
                    p.velocity -= viscosityForce * 0.5f;
                    n.velocity += viscosityForce * 0.5f;
                }
            }
        }
    }
    void Update()
    {
        particles.Clear();
        foreach (Transform child in transform)
        {
            particles.Add(child.GetComponent<FluidParticle>());
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x, y].Clear();
            }
        }
        foreach (FluidParticle p in particles)
        {
            p.gridX = (int)((p.position.x - xMin) / (xMax - xMin) * gridSizeX);
            p.gridY = (int)((p.position.y - yMin) / (yMax - yMin) * gridSizeY);

            if (p.gridX >= 0 && p.gridX < gridSizeX && p.gridY >= 0 && p.gridY < gridSizeY)
            {
                grid[p.gridX, p.gridY].Add(p);
            }
        }
        foreach (FluidParticle p in particles)
        {
            p.UpdateState();
        }
        CalculateDensity(particles);
        foreach (FluidParticle p in particles)
        {
            p.CalculatePressure();
        }
        CreatePressure(particles);
        CalculateViscocity(particles);
    }
}

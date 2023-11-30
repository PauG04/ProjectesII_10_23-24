using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

[CreateAssetMenu(fileName = "New Fluid Configuration", menuName = "Fluid Configuration", order = 0)]
public class FluidConfig : ScriptableObject
{
    [Header("Simulation Parameters")]
    public float simulationWidth = 0.5f;
    public float simulationGround = -2f;
    public float deltaTime = 20f;
    public float wallPosition = 0.08f;

    [Header("Physics Paramenters")]
    public float gravity = 0.02f * 0.25f;
    public float spacing = 0.08f; // Spacing between particles, used to calculate pressure
    public float K = 0.00008f; // Pressure factor
    public float KNear = 0.0008f; // Near pressure factor, pressure when particles are close to each other
    public float restDensity = 3f;
    public float radius = 1.25f; // Neighbour radius, if the distance between two particles is less than R, they are neighbours
    public float viscocity = 0.2f;
    public float maxVelocity = 0.25f;
    public float wallDamp = 0.2f; // Wall constraint factor, how much the particle is pushed away from the simulation walls

    private void OnEnable()
    {
        /*
        K = spacing / 10000;
        KNear = K * 100f;
        radius = spacing * 1.25f;
        */
    }
}

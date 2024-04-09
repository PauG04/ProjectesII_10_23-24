using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float earthIntensity;
    [SerializeField] private float shakeTime;

    private float timer;

    public bool shakeCamera;

    private Quaternion starterRot;
    private Vector3 starterVector;

    private void Awake()
    {
        starterRot = transform.rotation;
        starterVector = transform.position;
    }

    public void ShakeCamera(float intensity)
    {
        transform.rotation = Quaternion.Euler(starterRot.x, starterRot.y, starterRot.z + Random.Range(-intensity, intensity));
        transform.position = new Vector3(starterVector.x + Random.Range(-earthIntensity, earthIntensity), starterVector.y + Random.Range(-earthIntensity, earthIntensity), starterVector.z);

        timer += Time.deltaTime;

        if (timer >= shakeTime)
        {
            SetTransforPosition();
        }
    }

    public void SetTransforPosition()
    {
        shakeCamera = false;
        timer = 0;
        transform.rotation = starterRot;
        transform.position = starterVector;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private float shakeIntensity;
    [SerializeField]
    private float EarthIntensity;
    [SerializeField]
    private float shakeTime;

    [SerializeField]
    private float timer;

    public bool shakeCamera;

    private Quaternion starterRot;
    private Vector3 starterVector;

    private void Awake()
    {
        starterRot = transform.rotation;
        starterVector = transform.position;
    }

    public void ShakeCamera(float i)
    {
        shakeIntensity = i;
            transform.rotation = Quaternion.Euler(starterRot.x, starterRot.y, starterRot.z + Random.Range(-shakeIntensity, shakeIntensity));
            transform.position = new Vector3(starterVector.x + Random.Range(-EarthIntensity, EarthIntensity), starterVector.y + Random.Range(-EarthIntensity, EarthIntensity), starterVector.z);

            timer += Time.deltaTime;

            if (timer >= shakeTime)
            {
                shakeCamera = false;
                timer = 0;
                transform.rotation = starterRot;
                transform.position = starterVector;
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

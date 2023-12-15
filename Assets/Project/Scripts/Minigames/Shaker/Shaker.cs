using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float divideProgress;
    [SerializeField] private float maxProgress;
    [SerializeField] private float IntensityShaking;

    private DrinkScript _isPressing;
    private LiquidManager liquidManager;
    private Rigidbody2D rb;
    private bool isDown;
    private bool canShake;
    private Vector2 newPosition;
    private float progress;

    private void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        rb= GetComponent<Rigidbody2D>();
        _isPressing = GetComponent<DrinkScript>();
        liquidManager = GetComponent<LiquidManager>();
        canShake = false;
        newPosition = transform.position;
    }

    private void Update()
    {
        StartShaking();
        EndClicking();
        if (canShake && progress <= maxProgress)
        {
            DirectionShaker();
            IncreaseBar();
        }
        else
        {
            _cameraShake.SetTransforPosition();
        }
        //SetDrinkState();
    }

    private void StartShaking()
    {
        if ((rb.velocity.y >= 0.00001f || rb.velocity.y <= -0.00001f) && !_isPressing.GetIsMouseNotPressed())
        {
            canShake = true;
        }   
    }
    private void EndClicking()
    {
        if((rb.velocity.y <= 0.00001f && rb.velocity.y >= -0.00001f) || progress >= maxProgress)
        {
            canShake = false;
        }
    }

    private void DirectionShaker()
    {
        isDown = !(isDown && rb.velocity.y >= 0 && canShake);
    }

    private void IncreaseBar()
    {
        if(isDown)
        {
            progress += (newPosition.y - transform.position.y) / divideProgress;
        }
        else
        {
            progress += (transform.position.y - newPosition.y) / divideProgress;
        }
        _cameraShake.ShakeCamera((transform.position.y - newPosition.y) * IntensityShaking);
        newPosition = transform.position;
    }

    private void SetDrinkState()
    {
        if(progress < maxProgress * 0.33f)
        {
            liquidManager.SetDrinkState(LiquidManager.DrinkState.Idle);
        }
        else if(progress >= maxProgress * 0.66f)
        {
            liquidManager.SetDrinkState(LiquidManager.DrinkState.Mixed);
        }
        else
        {
            liquidManager.SetDrinkState(LiquidManager.DrinkState.Shaked);
        }
    }

    public float GetProgess()
    {
        return progress;
    }

    public float GetMaxProgress()
    { 
        return maxProgress; 
    }

    public void ResetShaker()
    {
        progress = 0;
        liquidManager.SetDrinkState(LiquidManager.DrinkState.Idle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float divideProgress;
    [SerializeField] private float maxProgress;

    private DrinkScript _isPressing;
    private Drink drink;
    private Rigidbody2D rb;
    private bool isDown;
    private bool canShake;
    private Vector2 newPosition;
    private float progress;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        _isPressing = GetComponent<DrinkScript>();
        drink = GetComponent<Drink>();
        canShake = false;
        newPosition = transform.position;
    }

    private void Update()
    {
        StartShaking();
        EndClicking();
        if (canShake)
        {
            DirectionShaker();
            IncreaseBar(isDown);
        }
        SetDrinkState();
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

    private void IncreaseBar(bool down)
    {
        if(down)
        {
            progress += (newPosition.y - transform.position.y) / divideProgress;
        }
        else
        {
            progress += (transform.position.y - newPosition.y) / divideProgress;
        }
        newPosition = transform.position;
    }

    private void SetDrinkState()
    {
        if(progress < maxProgress/2.0f)
        {
            drink.SetDrinkState(Drink.DrinkState.Idle);
        }
        else if(progress >= maxProgress)
        {
            drink.SetDrinkState(Drink.DrinkState.Mixed);
        }
        else
        {
            drink.SetDrinkState(Drink.DrinkState.Shaked);
        }
    }
}

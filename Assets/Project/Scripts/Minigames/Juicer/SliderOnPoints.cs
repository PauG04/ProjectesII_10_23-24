using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderOnPoints : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float maxSlider;
    [SerializeField] private float minVelocity;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private float diference;

    [SerializeField] private float valueSlider;
    private Vector3 position;
    private Click click;
    private bool isUp;
    private bool isRight;
    private bool isOut;
    private bool isFull;

    private void Start()
    {
        click = GetComponent<Click>();
        valueSlider = 0;
        position = transform.position;
        isUp = true;
        isRight = true;
        isFull = false;
    }

    private void Update()
    {
        isOut = CalculatePosition();
        SetPosition();
        if (click.GetRigidbody2D().velocity.magnitude > minVelocity && isOut && !isFull)
        {
            valueSlider += (value * click.GetRigidbody2D().velocity.magnitude) / diference;
        }
        if(valueSlider > maxSlider)
        {
            isFull = true;
        }
    }

    private bool CalculatePosition()
    {
        if (position.x <= transform.position.x + distanceX && position.x >= transform.position.x - distanceX)
        {
            return false;
        }
        if (position.y <= transform.position.y + distanceY && position.y >= transform.position.y - distanceY)
        {
            return false;
        }

        return true;
    }

    private void SetPosition()
    {
        if (isUp && click.GetRigidbody2D().velocity.y < 0)
        {
            isUp = false;
            position = transform.position;
        }
        else if (!isUp && click.GetRigidbody2D().velocity.y > 0)
        {
            isUp = true;
            position = transform.position;
        }
        if (isRight && click.GetRigidbody2D().velocity.x < 0)
        {
            isRight = false;
            position = transform.position;
        }
        else if (!isRight && click.GetRigidbody2D().velocity.x > 0)
        {
            isRight = true;
            position = transform.position;
        }
    }

    public bool GetIsFull()
    {
        return isFull;
    }

    public bool GetIsOut()
    {
        return isOut;
    }

}

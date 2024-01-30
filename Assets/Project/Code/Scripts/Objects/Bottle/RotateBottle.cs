using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBottle : MonoBehaviour
{
    private float _rotationSpeed = 10f;
    private float _maxRotation = 180f;

    private bool _isRotating = false;
    private float _targetRotation = 0f;
    private float _currentRotation = 0f;

    private DragItem dragItem;

    private void Start()
    {
        dragItem = GetComponent<DragItem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && dragItem.GetIsDraggin())
        {
            _isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _isRotating = false;
        }

        if (_isRotating)
        {
            RotateObject();
        }
        else
        {
            if (transform.rotation != Quaternion.identity)
            {
                ResetObjectPosition();
            }
        }

    }

    private void RotateObject()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        _targetRotation += mouseY * _rotationSpeed;
        _targetRotation = Mathf.Clamp(_targetRotation, 0, _maxRotation);

        _currentRotation = Mathf.Lerp(_currentRotation, -_targetRotation, Time.deltaTime * _rotationSpeed);
        transform.rotation = Quaternion.Euler(Vector3.forward * _currentRotation);
    }

    public void ResetObjectPosition()
    {
        _targetRotation = 0f;
        _currentRotation = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);
    }
}

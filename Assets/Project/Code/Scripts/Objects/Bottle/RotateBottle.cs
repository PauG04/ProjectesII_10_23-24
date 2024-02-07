using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBottle : MonoBehaviour
{
    private float _maxRotation = 180f;

    private bool _isRotating = false;
    private float _targetRotation = 0f;
    private float _currentRotation = 0f;

    private DragItem dragItem;

    [Header("Rotation")]
    [SerializeField]
    private float _rotationSpeed;

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

        if (Input.GetMouseButtonUp(1) || !dragItem.GetIsDraggin())
        {
            _isRotating = false;
        }

        if (_isRotating)
        {
            RotateObject();
        }
        else
        {
            if (transform.localRotation != Quaternion.identity)
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
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);
    }

    public bool GetIsRotating()
    {
        return _isRotating;
    }

    public float GetRotation()
    {
        return _currentRotation;
    }
}

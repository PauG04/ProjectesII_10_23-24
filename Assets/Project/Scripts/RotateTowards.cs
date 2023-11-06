using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public GameObject shaker;
    public Vector2 shakerPosition;

    private bool isShakerSpawned;
    [SerializeField] private Quaternion startedRotation;
    [SerializeField] private Vector3 startedScale;

    private void Update()
    {
        if (isShakerSpawned)
        {
            transform.right = (shakerPosition - (Vector2)transform.position).normalized;
        }
        else
        {
            if (startedScale != Vector3.zero)
            {
                transform.localScale = startedScale;
            }

        }
    }
    private void OnMouseDown()
    {
        if (GameObject.Find("Shaker") != null)
        {
            startedScale = transform.localScale;
            startedRotation = transform.rotation;
            shaker = GameObject.Find("Shaker");
            shakerPosition = shaker.transform.position;
            isShakerSpawned = true;
        }
    }
    private void OnMouseUp()
    {
        if (shaker != null)
        {
            isShakerSpawned = false;
            shakerPosition = Vector2.zero;
            transform.rotation = startedRotation;
        }
    }
}

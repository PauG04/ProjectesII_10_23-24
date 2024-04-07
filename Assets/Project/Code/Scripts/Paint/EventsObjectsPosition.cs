using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsObjectsPosition : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -2);
    }
}

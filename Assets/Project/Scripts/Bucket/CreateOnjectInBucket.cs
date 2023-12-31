using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnjectInBucket : MonoBehaviour
{
    [SerializeField] private GameObject objectInBucket;

    private void OnMouseDown()
    {
        GameObject newGlass1 = Instantiate(objectInBucket, transform);
        newGlass1.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }
}

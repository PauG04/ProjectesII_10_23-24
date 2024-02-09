using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject createdObject;

    [Header("Bucket")]
    [SerializeField] private GameObject bucket;
    private void OnMouseDown()
    {
        GameObject item = Instantiate(createdObject, transform);
        item.transform.SetParent(null);
        item.transform.localScale = createdObject.transform.localScale;
        item.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        item.GetComponent<DragItem>().SetIsDragging(true);
        
        if(bucket != null)
        {
            item.GetComponent<GetBucket>().SetBucket(bucket);
        }
        
        
    }
}

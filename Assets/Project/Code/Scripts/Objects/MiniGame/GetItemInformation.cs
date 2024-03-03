using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemInformation : MonoBehaviour
{
    [SerializeField] private ItemGroupNode itemGroupNode;
    private GameObject bucket;

    public ItemGroupNode GetItemGroupNode()
    {
        return itemGroupNode;
    }

    public void SetBucket(GameObject _bucket)
    {
        bucket = _bucket;   
    }

    public GameObject GetBuckets()
    {
        return bucket;
    }
}

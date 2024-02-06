using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBucket : MonoBehaviour
{
    private GameObject bucket;

    public void SetBucket(GameObject _bucket)
    {
        bucket = _bucket;   
    }

    public GameObject GetBuckets()
    {
        return bucket;
    }
}

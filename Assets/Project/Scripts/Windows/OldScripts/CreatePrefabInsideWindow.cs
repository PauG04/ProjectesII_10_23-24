using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefabInsideWindow : MonoBehaviour
{
    public GameObject prefab;

    private void Awake()
    {
        GameObject prefabInstance = Instantiate(prefab);
        prefabInstance.transform.parent = transform;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}

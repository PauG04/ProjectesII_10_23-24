using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIcePosition : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Item blockIce;

    private void Start()
    {
        InventoryManager.instance.UseItem(blockIce);
    }
    private void Update()
    {
        if(transform.localPosition != position)
        {
            transform.localPosition = position;
        }
    }
}

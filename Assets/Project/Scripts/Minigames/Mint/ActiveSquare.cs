using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSquare : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    [SerializeField] private GameObject square;
    [SerializeField] private GameObject[] childs;
    [SerializeField] private Item mintSpring;

    private float time = 0;
    private float maxTime = 0.5f;

    private void Start()
    {
        InventoryManager.instance.UseItem(mintSpring);
    }
    private void Update()
    {
        if(transform.localScale == scale)
        {
            time += Time.deltaTime;
            if(time>=maxTime)
            {
                square.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer") && collision.GetComponentInParent<DragHammer>().GetIsOut())
        {
            for(int i = 0; i< childs.Length; i++) 
            {
                if (childs[i] != null)
                    childs[i].GetComponent<TargetJoint2D>().enabled = false;
            }
        }
    }
}

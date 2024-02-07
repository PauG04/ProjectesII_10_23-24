using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BreakIce : MonoBehaviour
{
    private int hits;

    [Header("object")]
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject itemTotallyBroekn;
    [SerializeField] private float forceX;
    [SerializeField] private float forceY;
    [SerializeField] private float forceZ;

    private GameObject[] brokenIceChilds;

    private GameObject bucket;

    private void Start()
    {
        hits = 5;
        bucket = GetComponent<GetBucket>().GetBuckets();

        brokenIceChilds = new GameObject[4];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hammer") && gameObject.GetComponent<DragItem>().GetIsInWorkSpace())
        {
            hits--;
            if (hits == 0)
            {
                Slice(collision.transform.position, itemTotallyBroekn, true);
            }
            else
            {
                Slice(collision.transform.position, item, false);
            }          
        }
    }

    void Slice(Vector3 pos, GameObject createGameObject, bool destroy)
    {
        GameObject newItem = Instantiate(createGameObject, transform);
        newItem.transform.position = transform.position;

        if(destroy)
        {
            int j = 0;
            foreach (Transform child in newItem.transform)
            {
                
                brokenIceChilds[j] = child.gameObject;
                j++;
            }
            for(int i = brokenIceChilds.Length - 1; i>=0; i--)
            {
                brokenIceChilds[i].transform.SetParent(null);
                Rigidbody2D rbLemon = brokenIceChilds[i].GetComponent<Rigidbody2D>();
                rbLemon.AddForceAtPosition(new Vector3(Random.Range(forceX, -forceX), Random.Range(forceY, -forceY), Random.Range(forceZ, -forceZ)), pos, ForceMode2D.Impulse);
                brokenIceChilds[i].GetComponent<TakeItemToBucket>().SetBucket(bucket);
            }
        }
        else
        {
            newItem.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3(Random.Range(forceX, -forceX), Random.Range(forceY, -forceY), Random.Range(forceZ, -forceZ)), pos, ForceMode2D.Impulse);
            newItem.GetComponent<TakeItemToBucket>().SetBucket(bucket);
        } 
        
        newItem.transform.parent = null;

        if(destroy)
        {
            Destroy(gameObject);
            Destroy(newItem);
        }           
    }
}

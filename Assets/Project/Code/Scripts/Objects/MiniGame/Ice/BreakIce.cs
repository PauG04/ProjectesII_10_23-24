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

    private GameObject[] brokenIceChilds;

    private GameObject bucket;

    private float widht;
    private float height;

    private void Start()
    {
        hits = 5;
        bucket = GetComponent<GetBucket>().GetBuckets();

        brokenIceChilds = new GameObject[4];

        widht = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hammer"))
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
        newItem.transform.parent = null;

        if (!destroy)
        {
            float randomValue = Random.Range(0, 2);
            if(randomValue == 0)
            {
                newItem.transform.position = new Vector3(transform.position.x - widht , transform.position.y + height/2, 0);
                newItem.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3(Random.Range(forceX, forceX / 2), 0, 0), pos, ForceMode2D.Force);
                
            }
            else
            {
                newItem.transform.position = new Vector3(transform.position.x + widht, transform.position.y + height / 2, 0);
                newItem.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3(Random.Range(-forceX, -forceX / 2), 0, -0), pos, ForceMode2D.Force);
            }
            newItem.GetComponent<TakeItemToBucket>().SetBucket(bucket);
        }
        else
        {
            newItem.transform.position = transform.position;

            int j = 0;
            foreach (Transform child in newItem.transform)
            {
                brokenIceChilds[j] = child.gameObject;
                j++;
            }
            for (int i = brokenIceChilds.Length - 1; i >= 0; i--)
            {
                brokenIceChilds[i].transform.SetParent(null);
                Rigidbody2D rbIce = brokenIceChilds[i].GetComponent<Rigidbody2D>();
                rbIce.AddForceAtPosition(new Vector3(Random.Range(forceX * 15, -forceX * 15), 0,0), pos, ForceMode2D.Force);
                brokenIceChilds[i].GetComponent<TakeItemToBucket>().SetBucket(bucket);
            }

            Destroy(gameObject);
            Destroy(newItem);
        }
        
    }
}

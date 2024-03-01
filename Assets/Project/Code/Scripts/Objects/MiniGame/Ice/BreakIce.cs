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

    private int iceDropped;

    private void Start()
    {
        hits = 5;
        iceDropped = 0;
        bucket = GetComponent<GetItemInformation>().GetBuckets();

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
        AudioManager.instance.Play("BreakIce");
        GameObject newItem = Instantiate(createGameObject, transform);
        newItem.transform.parent = null;
        iceDropped++;

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
            newItem.GetComponent<DragItems>().SetInitPosition(bucket.transform.position);
            newItem.GetComponent<DragItems>().SetIsInWorkSpace(true);
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
                brokenIceChilds[i].GetComponent<DragItems>().SetInitPosition(bucket.transform.position);
                brokenIceChilds[i].GetComponent<DragItems>().SetIsInWorkSpace(true);
            }

            Destroy(gameObject);
            Destroy(newItem);
        }
        
    }

    public int GetIceDropped()
    {
        return iceDropped;
    }
}

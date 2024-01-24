using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        hits = 5;
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
            Slice(collision.transform.position, item, false);
        }
    }

    void Slice(Vector3 pos, GameObject createGameObject, bool destroy)
    {
        GameObject newItem = Instantiate(createGameObject, transform);
        newItem.transform.position = transform.position;

        //Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));
        //newItem.transform.localRotation = rotation;

        Vector3 dir = transform.position - pos;
        newItem.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3(Random.Range(forceX,-forceX), Random.Range(forceY, -forceY), Random.Range(forceZ, -forceZ)), pos, ForceMode2D.Impulse);

        //foreach (Transform slice in newItem.transform)
        //{
        //    Rigidbody2D rbLemon = slice.GetComponent<Rigidbody2D>();
        //    Vector3 dir = slice.transform.position - pos;
        //    rbLemon.AddForceAtPosition(dir.normalized * Random.Range(-force, force), pos, ForceMode2D.Impulse);
        //}
        newItem.transform.parent = null;
        

        if(destroy)
            Destroy(gameObject);
    }
}

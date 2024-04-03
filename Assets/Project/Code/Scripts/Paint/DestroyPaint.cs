using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEditor.Progress;

public class DestroyPaint : MonoBehaviour
{
    private int hits = 3;
    [SerializeField] private GameObject brokenPaint;
    [SerializeField] private float forceX;

    private GameObject[] brokenPaintChilds;

    private void Awake()
    {
        brokenPaintChilds = new GameObject[6];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            hits--;
            if (hits == 0)
            {
                Slice(collision.gameObject.transform.position);
            }

        }
    }

    void Slice(Vector3 pos)
    {
        GameObject newItem = Instantiate(brokenPaint, transform);
        newItem.transform.parent = null;
        newItem.transform.position = transform.position;

        int j = 0;
        foreach (Transform child in newItem.transform)
        {
            brokenPaintChilds[j] = child.gameObject;
            j++;
        }
        for (int i = brokenPaintChilds.Length - 1; i >= 0; i--)
        {
            brokenPaintChilds[i].transform.SetParent(null);
            Rigidbody2D rbIce = brokenPaintChilds[i].GetComponent<Rigidbody2D>();
            rbIce.AddForceAtPosition(new Vector3(Random.Range(forceX * 15, -forceX * 15), 0, 0), pos, ForceMode2D.Force);
        }
        Destroy(gameObject);
        Destroy(newItem);

    }
}

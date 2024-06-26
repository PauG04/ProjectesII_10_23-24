using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    [Header("Broken Glass")]
    [SerializeField] private GameObject brokenGlass;
    [SerializeField] private GameObject particles;
    private GameObject[] brokenChilds;

    [Header("Force")]
    [SerializeField] private float forceX;
    [SerializeField] private float forceY;


    private float hits;

    private void Start()
    {
        hits = 3;
        brokenChilds = new GameObject[4];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            hits--;
            if (hits == 0)
            {
                AudioManager.instance.PlaySFX("BreakGlass");
                Slice(transform.localPosition, brokenGlass);
            }
        }
    }

    void Slice(Vector3 pos, GameObject createGameObject)
    {
        GameObject newItem = Instantiate(createGameObject, transform);
        newItem.transform.parent = null;
        newItem.transform.position = transform.position;

        int j = 0;
        foreach (Transform child in newItem.transform)
        {
            brokenChilds[j] = child.gameObject;
            j++;
        }
        for (int i = brokenChilds.Length - 1; i >= 0; i--)
        {
            brokenChilds[i].transform.SetParent(null);
            Rigidbody2D rbIce = brokenChilds[i].GetComponent<Rigidbody2D>();
            rbIce.AddForceAtPosition(new Vector3(Random.Range(forceX * 15, -forceX * 15), forceY, 0), pos, ForceMode2D.Force);
        }
        for (int i = 0; i < gameObject.GetComponentInChildren<LiquidManager>().GetCurrentLiquid() / 2; i++)
        {
            GameObject _particles = Instantiate(particles, transform);
            _particles.transform.SetParent(null);
        }

        Destroy(gameObject);
        Destroy(newItem);
    }
}

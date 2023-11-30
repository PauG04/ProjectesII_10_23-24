using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideLemon : MonoBehaviour
{
    [SerializeField] private GameObject LemonSlide;
    [SerializeField] private float force;
    private Rigidbody2D rb;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slider"))
        {
            Slider slider = collision.gameObject.GetComponent<Slider>();
            Slice(transform.position, transform.position);
        }
    }

    void Slice(Vector3 direction, Vector3 pos)
    {
        GameObject newLemon = Instantiate(LemonSlide, transform);

        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));
        newLemon.transform.localRotation = rotation;

        foreach (Transform slice in newLemon.transform)
        {
            Rigidbody2D rbLemon = slice.GetComponent<Rigidbody2D>();
            Vector3 dir = slice.transform.position - pos;
            rbLemon.AddForceAtPosition(dir.normalized * Random.Range(-force, force), pos, ForceMode2D.Impulse);
        }
        newLemon.transform.parent = null;
        newLemon.transform.position = transform.position;
        Destroy(newLemon, 5f);

        Destroy(gameObject);
    }
}

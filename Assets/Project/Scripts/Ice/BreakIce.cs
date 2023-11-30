using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BreakIce : MonoBehaviour
{
    private FixedJoint2D fixedJoint2;
    private Rigidbody2D rb;

    [SerializeField] private GameObject iceSlide;
    [SerializeField] private GameObject hammer;
    [SerializeField] private float force;

    private void Start()
    {
        fixedJoint2= GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hammer"))
        {
            Slice(hammer.transform.position, hammer.transform.position);
            Destroy(fixedJoint2);
        }
    }

    void Slice(Vector3 direction, Vector3 pos)
    {
        GameObject newIce = Instantiate(iceSlide, transform);

        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));
        newIce.transform.localRotation = rotation;

        foreach (Transform slice in newIce.transform)
        {
            Rigidbody2D rbLemon = slice.GetComponent<Rigidbody2D>();
            Vector3 dir = slice.transform.position - pos;
            rbLemon.AddForceAtPosition(dir.normalized * Random.Range(-force, force), pos, ForceMode2D.Impulse);
        }
        newIce.transform.parent = null;
        newIce.transform.position = transform.position;
        Destroy(newIce, 5f);

        Destroy(gameObject);
    }
}

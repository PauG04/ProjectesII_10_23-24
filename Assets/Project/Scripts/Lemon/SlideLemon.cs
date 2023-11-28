using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideLemon : MonoBehaviour
{
    [SerializeField] private GameObject LemonSlide;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Slider"))
        {
            Slider slider = collision.gameObject.GetComponent<Slider>();
            Slice(slider.GetDirection(), slider.transform.position);
        }
    }

    void Slice(Vector3 direction, Vector3 pos)
    {
        Debug.Log("si");
        LemonSlide.SetActive(true);
        Vector3 dirtectionRot = direction;
        if(direction.magnitude> 0) 
        {
            dirtectionRot = pos - transform.position;
        }

        Quaternion rotation = Quaternion.LookRotation(dirtectionRot.normalized);
        LemonSlide.transform.localRotation = rotation;

        foreach(Transform slice in LemonSlide.transform) 
        { 
            Rigidbody rbLemon = slice.GetComponent<Rigidbody>();
            rbLemon.velocity = rb.velocity;
            Vector3 dir = slice.transform.position - pos;
            rbLemon.AddForceAtPosition(dir.normalized, pos, ForceMode.Impulse);
        }
        LemonSlide.transform.parent = null;
        LemonSlide.transform.position = transform.position;
        DestroyImmediate(LemonSlide, true);

        Destroy(gameObject);


    }
}

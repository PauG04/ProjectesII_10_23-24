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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Slider"))
        {
            Slider slider = other.gameObject.GetComponent<Slider>();
            Slice(slider.transform.position, slider.transform.position);
        }
    }

    void Slice(Vector3 direction, Vector3 pos)
    {
        GameObject newLemon = Instantiate(LemonSlide, transform);
        newLemon.SetActive(true);
        Vector3 dirtectionRot = direction;
        if(direction.magnitude> 0) 
        {
            dirtectionRot = pos - transform.position;
        }

        Quaternion rotation = Quaternion.LookRotation(dirtectionRot.normalized);
        newLemon.transform.localRotation = rotation;

        foreach(Transform slice in newLemon.transform) 
        { 
            Rigidbody rbLemon = slice.GetComponent<Rigidbody>();
            rbLemon.velocity = rb.velocity;
            Vector3 dir = slice.transform.position - pos;
            rbLemon.AddForceAtPosition(dir.normalized, pos, ForceMode.Impulse);
        }
        newLemon.transform.parent = null;
        newLemon.transform.position = transform.position;
        Destroy(newLemon, 5f);

        Destroy(gameObject);
    }
}

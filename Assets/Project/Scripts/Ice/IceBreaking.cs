using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreaking : MonoBehaviour
{
    private bool destroy;

    [SerializeField] private Transform iceBroken;

    private void Update()
    {
        if (destroy)
        {
            Transform iceBrokenTransform = Instantiate(iceBroken, transform.position, transform.rotation);
            foreach (Transform child in iceBrokenTransform)
            {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(100f, transform.position, 5f);
                    childRigidbody.useGravity = true;
                }
            }
            Destroy(gameObject);
        }
    }

    public void BreakIce()
    {
        destroy = true;
    }

}

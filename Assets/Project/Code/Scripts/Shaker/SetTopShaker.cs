using System.Collections;
using UnityEngine;

public class SetTopShaker : MonoBehaviour
{
    [SerializeField] private DragPhysicObject target;
    [SerializeField] private float lerpSpeed = 2;
    private bool isTargetInside;
    private bool isShakerClosed;

    private bool isRepositioning = false;


    private void Update()
    {
        if (!target.GetMouseDown() && isTargetInside && !isShakerClosed && !isRepositioning)
        {
            target.transform.SetParent(transform.parent);
            StartCoroutine(RepositionCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            isTargetInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            isTargetInside = false;
            isShakerClosed = false;
        }

    }

    private IEnumerator RepositionCoroutine()
    {
        isRepositioning = true;
        
        float elapsedTime = 0f;
        Vector3 initialPosition = target.transform.position;

        while (elapsedTime < lerpSpeed)
        {
            float t = elapsedTime / lerpSpeed;
            target.transform.position = Vector3.Lerp(initialPosition, transform.position, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        target.transform.position = transform.position;

        isShakerClosed = true;
        isRepositioning = false;
    }

    public bool GetIsShakerClosed()
    {
        return isShakerClosed;
    }
}

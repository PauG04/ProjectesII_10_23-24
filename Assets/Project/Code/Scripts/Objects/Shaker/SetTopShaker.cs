using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SetTopShaker : MonoBehaviour
{
    [SerializeField] private DragItemsNew target;
    [SerializeField] private float lerpSpeed = 0.5f;

    private bool isTargetInside;
    private bool isRepositioning = false;
    private bool isAnimationDone = false;

    private void Update()
    {
        if (target.GetIsLerping())
        {
            isTargetInside = false;
            isRepositioning = false;
        }


        if (!target.GetIsDraggin() && isTargetInside && !isRepositioning)
        {
            if (!isAnimationDone)
            {
                StartCoroutine(RepositionCoroutine());
            }
            else
            {
                target.transform.position = transform.position;
                target.transform.rotation = transform.parent.rotation;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            target.SetBodyGravity(0);
            isTargetInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            if (target.GetIsDraggin() || !target.GetInsideWorkspace())
            {
                target.SetBodyGravity(1);
                isTargetInside = false;
                isAnimationDone = false;
            }
        }
    }
    private IEnumerator RepositionCoroutine()
    {
        isRepositioning = true;
        
        float elapsedTime = 0f;
        Vector3 initialPosition = target.transform.position;
        Quaternion initialRotation = target.transform.rotation;

        while (elapsedTime < lerpSpeed)
        {
            float t = elapsedTime / lerpSpeed;
            target.transform.position = Vector3.Lerp(initialPosition, transform.position, t);
            target.transform.rotation = Quaternion.Lerp(initialRotation, transform.rotation, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        target.transform.position = new Vector2(transform.position.x, transform.position.y);
        target.transform.rotation = transform.rotation;

        isRepositioning = false;
        isAnimationDone = true;
    }
    public bool GetIsShakerClosed()
    {
        return isTargetInside;
    }
}

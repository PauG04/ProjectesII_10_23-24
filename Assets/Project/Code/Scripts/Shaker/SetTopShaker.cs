using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SetTopShaker : MonoBehaviour
{
    [SerializeField] private DragPhysicObject target;
    [SerializeField] private float lerpSpeed = 2;

    private bool isTargetInside;
    private bool isRepositioning = false;
    private bool isAnimationDone = false;

    private void Update()
    {
        if (!target.GetMouseDown() && isTargetInside && !isRepositioning && !isAnimationDone)
        {
            StartCoroutine(RepositionCoroutine());
        }
        if(isTargetInside && isAnimationDone && !isRepositioning)
        {
            target.transform.position = transform.position;
            target.transform.rotation = transform.parent.rotation;
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
        if (collision.gameObject == target.gameObject && target.GetMouseDown())
        {
            isTargetInside = false;
            isAnimationDone = false;
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

        target.transform.position = new Vector2(transform.position.x, transform.position.y);

        isRepositioning = false;
        isAnimationDone = true;
    }
    public bool GetIsShakerClosed()
    {
        return isTargetInside;
    }
}

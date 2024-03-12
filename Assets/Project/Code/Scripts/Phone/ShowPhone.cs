using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPhone : MonoBehaviour
{
    [SerializeField] private RectTransform[] phoneComponents;

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3[] initialPositions;

    private bool isMoving = false;
    private bool isOpen = true;

    private void Start()
    {
        initialPositions = new Vector3[phoneComponents.Length];
        for (int i = 0; i < phoneComponents.Length; i++)
        {
            initialPositions[i] = phoneComponents[i].anchoredPosition;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveComponents());
        }
    }

    private IEnumerator MoveComponents()
    {
        Vector2 startPosition = isOpen ? initialPositions[0] : targetPosition;
        Vector2 endPosition = isOpen ? targetPosition : initialPositions[0];

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            for (int i = 0; i < phoneComponents.Length; i++)
            {
                phoneComponents[i].anchoredPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            }
            yield return null;
        }

        for (int i = 0; i < phoneComponents.Length; i++)
        {
            phoneComponents[i].anchoredPosition = endPosition;
        }

        isMoving = false;
        isOpen = !isOpen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGlass : MonoBehaviour
{
    [SerializeField] private GameObject glass;

    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Instantiate(glass, mousePosition, Quaternion.identity);
        glass.GetComponent<DragItems>().ObjectPressed();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGlass : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private DragItems dragObject;

    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject newGlass = Instantiate(objectToSpawn, mousePosition, Quaternion.identity);
        dragObject = newGlass.GetComponent<DragItems>();
        dragObject.SetIsDragging(true);
        dragObject.ObjectPressed();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGlass : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private DragItems dragObject;

    [SerializeField] private FriendEvent tutorial;
    private bool isCreated;

    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject newGlass = Instantiate(objectToSpawn, mousePosition, Quaternion.identity);
        dragObject = newGlass.GetComponent<DragItems>();
        dragObject.SetIsDragging(true);
        dragObject.ObjectPressed();
        isCreated = true;

        if(tutorial != null)
        {
            tutorial.SetGlass(newGlass.transform.GetChild(4).gameObject);
        }
    }

    public bool GetIsCreated()
    {
        return isCreated;
    }
}

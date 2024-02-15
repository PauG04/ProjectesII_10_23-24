using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutLemon : MonoBehaviour
{
    [Header("Father")]
    [SerializeField] private SetCutPosition childs;
    [SerializeField] private DragItemsNew lemon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knife") && lemon.GetInsideWorkspace())
        {
            childs.FreeChild();
        }
    }
}

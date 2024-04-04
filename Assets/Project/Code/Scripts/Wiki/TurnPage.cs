using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    private void OnMouseDown()
    {
        if (isLeft)
            WikiManager.instance.PrevPage();
        else
            WikiManager.instance.NextPage();
    }
}

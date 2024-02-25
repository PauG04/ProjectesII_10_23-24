using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(GetComponentInParent<WikiPage>().GetIsLeft())
            WikiManager.instance.PrevPage();
        else
            WikiManager.instance.NextPage();
    }
}

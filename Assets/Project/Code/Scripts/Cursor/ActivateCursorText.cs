using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivateCursorText : MonoBehaviour
{
    private void OnMouseOver()
    {
        CursorManager.instance.GetBox().SetActive(true);
        CursorManager.instance.GetItemName().text = gameObject.GetComponent<DropLiquid>().GetDrink().spanishName;
    }

    private void OnMouseExit()
    {
        CursorManager.instance.GetBox().SetActive(false);
        CursorManager.instance.GetItemName().text = "";
    }



}

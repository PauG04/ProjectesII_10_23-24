using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivateCursorText : MonoBehaviour
{
    [SerializeField] private bool isShop;
    private void OnMouseOver()
    {       
        if(isShop)
        {
            CursorManager.instance.GetItemName().text = gameObject.GetComponent<BuyLiquid>().GetPrice().ToString() + " $";
            CursorManager.instance.GetBox().SetActive(true);
        }
        else if(!GetComponent<DragItems>().GetInsideWorkspace())
        {
            CursorManager.instance.GetItemName().text = gameObject.GetComponent<DropLiquid>().GetDrink().spanishName;
            CursorManager.instance.GetBox().SetActive(true);
        }
        
    }

    private void OnMouseExit()
    {
        CursorManager.instance.GetBox().SetActive(false);
        CursorManager.instance.GetItemName().text = "";
    }



}

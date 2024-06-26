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
            if(EconomyManager.instance.GetMoney() > gameObject.GetComponent<BuyShop>().GetPrice())
            {
                CursorManager.instance.SetColor(Color.green);
            }
            else
            {
                CursorManager.instance.SetColor(Color.red);
            }
            CursorManager.instance.GetItemName().text = gameObject.GetComponent<BuyShop>().GetPrice().ToString() + "�";
            CursorManager.instance.GetBox().SetActive(true);
        }
        else if(GetComponent<DragItems>() != null)
        {
            if(!GetComponent<DragItems>().GetInsideWorkspace())
            {
                CursorManager.instance.GetItemName().text = gameObject.GetComponent<DropLiquid>().GetDrink().spanishName;
                CursorManager.instance.GetBox().SetActive(true);
            }

        }
        
    }

    private void OnMouseExit()
    {
        CursorManager.instance.GetBox().SetActive(false);
        CursorManager.instance.GetItemName().text = "";
        CursorManager.instance.SetColor(Color.black);
    }



}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderTaskBar : MonoBehaviour
{
    public GameObject icon;

    [SerializeField] private SetTaskBarPosition setTaskBarPosition;
    private int currentIndex;

    public void SetIcon()
    {
        //setTaskBarPosition.GetIcon()[setTaskBarPosition.GetpositionIndex()] = icon;
        setTaskBarPosition.GetList().Add(icon);
        //currentIndex = setTaskBarPosition.GetpositionIndex();
        //setTaskBarPosition.SetpositionIndex(1);
        //setTaskBarPosition.SetRefresh();
    }

    public void SetCloseIcon()
    {
        MoveIcon();
        //setTaskBarPosition.SetpositionIndex(-1);
        //setTaskBarPosition.SetRefresh();
    }

    private void MoveIcon()
    {
        //for (int i = currentIndex; i < setTaskBarPosition.GetList().Length - 1; i++)
        //{
        //    setTaskBarPosition.GetIcon()[i] = setTaskBarPosition.GetIcon()[i + 1];
        //}
        //setTaskBarPosition.SetCurrentIndex();
        setTaskBarPosition.GetList().Remove(icon);
        //setTaskBarPosition.GetIcon()[setTaskBarPosition.GetIcon().Length - 1] = null;

    }

    public int GetIndex()
    {
        return currentIndex;
    }

    public void SetIndex()
    {
        currentIndex--;
    }

}

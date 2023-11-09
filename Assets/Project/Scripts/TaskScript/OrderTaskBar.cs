using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderTaskBar : MonoBehaviour
{
    [SerializeField] private SetTaskBarPosition setTaskBarPosition;
    public GameObject icon;

    private int currentIndex;

    public void SetIcon()
    {
        setTaskBarPosition.GetList().Add(icon);
    }
    public void SetCloseIcon()
    {
        MoveIcon();
    }
    private void MoveIcon()
    {
        setTaskBarPosition.GetList().Remove(icon);
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

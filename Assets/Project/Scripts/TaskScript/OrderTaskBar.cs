using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTaskBar : MonoBehaviour
{
    private OpenApp openApp;
    [SerializeField]
    private SetTaskBarPosition setTaskBarPosition;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private int currentIndex;
    private bool isOpen = false;
    [SerializeField]
    private OrderTaskBar[] orderTaskBar;
    private void Awake()
    {
        openApp = GetComponent<OpenApp>();
    }

    public void SetIcon()
    {
        setTaskBarPosition.GetIcon()[setTaskBarPosition.GetpositionIndex()] = icon;
        currentIndex = setTaskBarPosition.GetpositionIndex();
        setTaskBarPosition.SetpositionIndex(1);
        setTaskBarPosition.SetRefresh();
    }

    public void SetCloseIcon()
    {
        MoveIcon();
        setTaskBarPosition.SetpositionIndex(-1);
        setTaskBarPosition.SetRefresh();
    }

    public void MoveIcon()
    {
        for (int i = currentIndex; i < setTaskBarPosition.GetIcon().Length - 1; i++)
        {
            setTaskBarPosition.GetIcon()[i] = setTaskBarPosition.GetIcon()[i + 1];          
        }
        for (int i = 0; i < orderTaskBar.Length; i++)
        {
            if (orderTaskBar[i].currentIndex != 0)
                orderTaskBar[i].currentIndex--;
        }
        setTaskBarPosition.GetIcon()[setTaskBarPosition.GetIcon().Length - 1] = null;
        
        
    }

}

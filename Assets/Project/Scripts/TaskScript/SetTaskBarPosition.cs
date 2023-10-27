using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SetTaskBarPosition : MonoBehaviour
{
    [SerializeField]
    private float xPosition;
    private float orignalX;
    [SerializeField]
    private OrderTaskBar[] orderTaskBar;
    private bool[] isOpen;
    private bool[] isPrinted;
    private SetMinimize setMinimize;
    [SerializeField]
    private GameObject[] orderIcon;
    private bool[] isRight;
    private int currentIcon = 0;
    [SerializeField]
    private float distance;

    private void Awake()
    {
        setMinimize= GetComponent<SetMinimize>();
    }

    private void Start()
    {
        orderIcon = new GameObject[orderTaskBar.Length];
        isOpen = new bool[orderTaskBar.Length];       
        isPrinted= new bool[orderTaskBar.Length];
        isRight = new bool[orderTaskBar.Length];
        for(int i = 0; i<isOpen.Length; i++) 
        {
            isOpen[i] = false;
            isPrinted[i] = false;
            isRight[i] = false;
        }
        orignalX = xPosition;
    }
    private void Update()
    {
        OpenApp();
        PrintIcon();
        CloseIcon();
    }

    private void OpenApp()
    {
        for (int i = 0; i < setMinimize.icon.Length; i++)
        {
            if (orderTaskBar[i].IsOpen() && !isOpen[i])
            {
                orderIcon[currentIcon] = setMinimize.icon[i];
                isPrinted[currentIcon] = true;
                isRight[currentIcon] = true;
                isOpen[i] = true;
                currentIcon++; 
            }
        }
    }

    private void PrintIcon()
    {
        for (int i = 0; i < orderIcon.Length; i++)
        {
            if (isPrinted[i] && isRight[i] && orderIcon[i] != null) 
            {
                orderIcon[i].transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
                orderIcon[i].SetActive(true);
                SetXPosition(distance);             
                isPrinted[i] = false;
                isRight[i] = false;
            }
        }
    }

    private void CloseIcon()
    {
        int currentI;
        for (int i = 0; i < setMinimize.icon.Length; i++)
        {
            currentI = 0;
            if (!orderTaskBar[i].IsOpen() && isOpen[i])
            {
                currentI = FinOrderIcon(setMinimize.icon[i]);
                OrderTask(currentI);
                SetIsRight(currentI);
                setOriginalX(setMinimize.icon[i].transform.position.x);
                SetIsPrinted();
                isOpen[i] = false;            
            }

        }

    }

    private void SetXPosition(float x)
    {
        xPosition += x;
    }

    private void setOriginalX(float position)
    {
        xPosition = position;
    }

    private int FinOrderIcon(GameObject icon)
    {
        int i = 0;
        while (icon.transform.position.x != orderIcon[i].transform.position.x)
        {
            i++;              
        }
        orderIcon[i].SetActive(false);
        return i;
    }

    private void OrderTask(int currentI)
    {
        for (int i = currentI; i < orderIcon.Length - 1; i++)
        {
            orderIcon[i] = orderIcon[i+1];
        }
        orderIcon[orderIcon.Length - 1] = null;
        currentIcon--;
    }

    private void SetIsPrinted()
    {
        for(int i = 0; i<orderIcon.Length; i++) 
        {
            if (orderIcon[i] != null)
                isPrinted[i] = true;
        }
    }

    private void SetIsRight(int currentI)
    {
        for (int i = currentI; i < orderIcon.Length; i++)
        {
            isRight[i] = true;
        }
    }

}


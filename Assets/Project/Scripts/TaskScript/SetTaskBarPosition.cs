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
    private GameObject[] positionsIcon;

    [SerializeField]
    private GameObject[] icon;
    [SerializeField]
    private int positionIndex;
    [SerializeField]
    private OrderTaskBar[] orderTaskBar;
    private bool refresh;

    private void Awake()
    {
        icon = new GameObject[positionsIcon.Length];
        for(int i = 0; i< positionsIcon.Length; i++)
        {
            icon[i] = positionsIcon[i];
        }
    }

    private void Update()
    {
       int j = 0;
       for(int i = 0; i< positionsIcon.Length; i++) 
       {
            if(refresh && icon[i] != null)
            {
                icon[i].transform.position = positionsIcon[j].transform.position;
                j++;
            }
       }
        refresh = false;
    }

    public void SetpositionIndex(int index)
    {
        positionIndex+= index;
    }

    public void SetRefresh()
    {
        refresh = true;
    }

    public GameObject[] GetIcon()
    {
        return icon;
    }

    public int GetpositionIndex() 
    { 
        return positionIndex; 
    }

    public void SetCurrentIndex()
    {
        for (int i = 0; i < orderTaskBar.Length; i++)
        {
            if (orderTaskBar[i].GetIndex() != 0)
                orderTaskBar[i].SetIndex();
        }
    }

}


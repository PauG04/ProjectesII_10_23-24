using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SetTaskBarPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] positionsIcon;

    [SerializeField] private List<GameObject> icon;
    [SerializeField] private int positionIndex;
    [SerializeField] private List<OrderTaskBar> orderTaskBar;

    private void Update()
    {
       int j = 0;
       for(int i = 0; i< icon.Count; i++) 
       {
            icon[i].transform.position = positionsIcon[j].transform.position;
            j++;          
       }
    }
    public List<GameObject> GetList()
    {
        return icon;
    }
}


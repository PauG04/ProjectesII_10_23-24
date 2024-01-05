using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetMiniGame : MonoBehaviour
{
    [SerializeField] private GameObject pivot;
    [SerializeField] private TextMeshPro text;
    
    private GameObject glass;
    private IsEmpty isEmpty;

    private void Start()
    {        
        isEmpty = pivot.GetComponent<IsEmpty>();
    }

    private void Update()
    {
        if(glass == null)
        {
            glass = isEmpty.GetGlass();
        }
       
        
    }
    private void OnMouseDown()
    {
        if(!isEmpty.GetIsEmpty())
        {
            isEmpty.SetIsEmpty(true);
            text.text = null;
            Destroy(glass);
        }
        
    }
}

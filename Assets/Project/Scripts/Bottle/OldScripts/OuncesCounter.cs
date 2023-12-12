using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuncesCounter : MonoBehaviour
{
    private int counter = 0;

    public void AddOneToCounter()
    {
        counter++;
    }

    public void RestartCounter()
    {
        counter = 0;
    }

    
}

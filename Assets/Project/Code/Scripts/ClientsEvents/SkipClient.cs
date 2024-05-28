using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipClient : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ClientManager.instance.PassClient();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        textMesh.text = TimeManager.instance.GetHour().ToString("00") + ":" + TimeManager.instance.GetMinute().ToString("00");
    }
}

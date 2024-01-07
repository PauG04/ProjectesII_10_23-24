using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndOfDayText : MonoBehaviour
{
    private TextMeshProUGUI titleText;

    private void Awake()
    {
        titleText = GetComponentInChildren<TextMeshProUGUI>();
    }

}

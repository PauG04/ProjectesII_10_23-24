using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RectLenght : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textMP;

    private float scaleTextLenght;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMP = GetComponent<TextMeshProUGUI>();
        scaleTextLenght = 50.0f;
    }

    //Llamar a esta funcion cada vez que se añada una linia de texto
    public void UpdateLenght()
    {
        float textLenght = textMP.textInfo.lineCount * scaleTextLenght;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textLenght);
    }
}

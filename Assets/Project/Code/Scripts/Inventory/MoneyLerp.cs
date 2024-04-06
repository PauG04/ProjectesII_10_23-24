using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyLerp : MonoBehaviour
{
    [SerializeField] private float VelocityY;
    [SerializeField] private float VelocityAlpha;

    void Update()
    {
        if (GetComponent<TextMeshPro>().color.a < 0.1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y + VelocityY, -1);

            Color newColor = GetComponent<TextMeshPro>().color;

            newColor.a = Mathf.Lerp(newColor.a, 0, Time.deltaTime * VelocityAlpha);

            GetComponent<TextMeshPro>().color = newColor;

        }


    }
}

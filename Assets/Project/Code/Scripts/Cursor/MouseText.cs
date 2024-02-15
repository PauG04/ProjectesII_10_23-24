using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorText : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private TextMeshPro textMP;

    private void Awake()
    {
        textMP = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        transform.position = CursorManager.instance.GetCursorPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RotateBottle>(out RotateBottle bottle))
        {
            Debug.Log("ENTRO");
            box.SetActive(true);
            textMP.text = collision.gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RotateBottle>(out RotateBottle bottle))
        {
            box.SetActive(false);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanLiquid : MonoBehaviour
{
    [Header("Saturation")]
    [SerializeField] private float difference;

    private Color _color;

    private void Awake()
    {
        _color = GetComponent<SpriteRenderer>().color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
            if(_color.g > 0.2f)
            {
                _color = new Color(_color.r - difference, _color.g - difference, _color.b - difference);
                GetComponent<SpriteRenderer>().color = _color;
            }           
        }
    }
}

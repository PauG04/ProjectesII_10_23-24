using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWindowsColor : MonoBehaviour
{
    [SerializeField] private float r;
    [SerializeField] private float g;
    [SerializeField] private float b;
    [SerializeField] private float alpha;
    [SerializeField] private SpriteRenderer childIcon;

    private void Awake()
    {
        childIcon.color = new Color(1, 1, 1, 0);
    }

    private void OnMouseOver()
    {
        childIcon.color = new Color(r, g, b, alpha);
    }

    private void OnMouseExit()
    {
        childIcon.color = new Color(1, 1, 1, 0);
    }
}

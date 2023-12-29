using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CreateGlass : MonoBehaviour
{
    [SerializeField] private GameObject glass;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        CreateNewGlass();
    }

    public void CreateNewGlass()
    {
        GameObject newGlass1 = Instantiate(glass, transform);
        newGlass1.transform.localPosition = new Vector2(newGlass1.transform.localPosition.x + spriteRenderer.bounds.size.x / 1.2f, newGlass1.transform.localPosition.y - spriteRenderer.bounds.size.y / 1.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLeaf : MonoBehaviour
{
    [SerializeField] private int maxLeaf;
    [SerializeField] private GameObject leaf;

    private bool isLeaft;
    private SpriteRenderer spriteRenderer;
    private bool isGenerate;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isLeaft = false;
        isGenerate = false;
    }

    private void Update()
    {
        if(!isGenerate)
        {
            for (int i = 0; i < maxLeaf; i++)
            {
                isLeaft = (Random.value > 0.5);
                CreateLeaf(isLeaft);
            }
            isGenerate = true;
        }      
    }

    private void CreateLeaf(bool isLeft)
    {
        Vector3 newPosition;

        if (isLeft) 
        {
            newPosition = new Vector3(transform.position.x - spriteRenderer.bounds.size.x / 1.5f, transform.position.y + Random.Range(-spriteRenderer.bounds.size.y / 2.5f, spriteRenderer.bounds.size.y / 2.5f), 0);
        }
        else
        {
            newPosition = new Vector3(transform.position.x + spriteRenderer.bounds.size.x / 1.5f, transform.position.y + Random.Range(-spriteRenderer.bounds.size.y / 2.5f, spriteRenderer.bounds.size.y / 2.5f), 0);
        }
        
        GameObject newLeaf = Instantiate(leaf, newPosition, Quaternion.identity);
        newLeaf.transform.parent = transform;
    }
}

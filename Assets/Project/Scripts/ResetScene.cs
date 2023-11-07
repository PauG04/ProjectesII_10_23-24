using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] private OpenStartMenu openStartMenu;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(openStartMenu.GetIsOpen())
        {
            SceneManager.LoadScene("Main");                     
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private GameObject[] buttons;

    private bool isOpen;
    private bool startLerp;
    private Vector2 startPosition;
    private Vector2 endPosition;
    

    private void Start()
    {
        isOpen = false;
        startPosition = child.transform.localPosition;
        endPosition = new Vector2(child.transform.localPosition.x, child.transform.localPosition.y + 70);
    }


    public void OpenConfiguration()
	{
        if(!isOpen)
        {
            child.transform.localPosition = endPosition;
            child.SetActive(true);
            child.GetComponent<SpriteRenderer>().sortingOrder = 1;
            isOpen = true;
            for(int i = 0; i<buttons.Length; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
        else
        {
            child.transform.localPosition = startPosition;
            child.SetActive(false);
            child.GetComponent<SpriteRenderer>().sortingOrder = 0;
            isOpen = false;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                buttons[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }

    }
}

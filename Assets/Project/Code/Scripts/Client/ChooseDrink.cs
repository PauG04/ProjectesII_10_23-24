using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.SceneManagement;

public class ChooseDrink : MonoBehaviour
{
    public Dictionary<CocktailNode.Type, Dialogue.Dialogue> type;

    public void ChangeScene()
    {
        Debug.Log("Recibed Trigger");
        SceneManager.LoadScene(1);
    }
}

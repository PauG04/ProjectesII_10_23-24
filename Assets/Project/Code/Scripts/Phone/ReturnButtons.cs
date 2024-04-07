using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtons : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    public void ExitToMainMenu()
    {
        levelLoader.ResetSave();
        levelLoader.LoadSpecificLevel(1);
    }
    public void ExitToDesktop()
    {
        levelLoader.ResetSave();
        levelLoader.CloseAnimation();
        Application.Quit();
    }
}

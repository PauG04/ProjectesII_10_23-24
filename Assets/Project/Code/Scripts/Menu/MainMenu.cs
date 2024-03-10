using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    public void PlayButton()
    {
        levelLoader.LoadNextLevel();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

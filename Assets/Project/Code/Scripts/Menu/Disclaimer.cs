using UnityEngine;
using UnityEngine.SceneManagement;

public class Disclaimer : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    private void Update()
    {
        if(Input.anyKey)
        {
            levelLoader.LoadNextLevel();
        }
    }
}

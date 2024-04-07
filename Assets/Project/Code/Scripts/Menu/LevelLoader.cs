using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    private static bool loadSave = false;
    private SaveComponents saveComponents;

    private void Awake()
    {
        saveComponents = GetComponent<SaveComponents>();

        if (loadSave)
        {
            saveComponents.LoadAllComponents();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadSpecificLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
    public void Save()
    {
        saveComponents.SaveAllComponents();
    }
    public void Load()
    {
        saveComponents.LoadAllComponents();
    }
    public void LoadSave()
    {
        loadSave = true;
    }
    public void ResetSave()
    {
        loadSave = false;
    }
    public void CloseAnimation()
    {
        transition.SetTrigger("Start");
    }
    public void OpenAnimation()
    {
        transition.SetTrigger("End");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VFolders.Libs;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    private bool loadSave = false;
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
    public void SetLoadSave(bool loadSave)
    {
        this.loadSave = loadSave;
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

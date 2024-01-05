using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerOffButton : MonoBehaviour
{
    [SerializeField] private GameObject desktop;
    private bool startLerp;
    private void OnMouseDown()
    {
        startLerp = true;
    }

    private void Update()
    {
        if(startLerp)
        {
            desktop.transform.localScale = Vector3.Lerp(desktop.transform.localScale, Vector3.zero, 4 * Time.deltaTime);
            if(desktop.transform.localScale.y < 0 + 0.01)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                SceneManager.LoadScene("UserScene");
                startLerp = false;
            }
        }
    }
}

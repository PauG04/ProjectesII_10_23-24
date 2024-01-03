using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterUser : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("MarcosScene2");
    }
}

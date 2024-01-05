using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate3Points : MonoBehaviour
{
    [SerializeField] private float maxRotationVelocity;
    [SerializeField] private float maxTime;

    private float time;

    private void Update()
    {
        transform.Rotate(0, 0, maxRotationVelocity);
        TimerScene();
    }

    private void TimerScene()
    {
        time += Time.deltaTime;
        if(time> maxTime)
        {
            SceneManager.LoadScene("UserScene");
        }
    }

}

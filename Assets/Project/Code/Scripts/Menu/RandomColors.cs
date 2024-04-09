using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class RandomColors : MonoBehaviour
{
    [SerializeField] private float timer = 0.1f;
    private TextMeshProUGUI m_TextMeshPro;

    private Color color;
    private float time;

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            m_TextMeshPro.color = color;
            time = 0;
        }
    }
}

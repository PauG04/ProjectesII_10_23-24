using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private GameObject healthSlider;
    [SerializeField] private GameObject stressSlider;
    [SerializeField] private GameObject fatigueSlider;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateSliders();
    }

    private void Update()
    {
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        healthSlider.transform.localScale = new Vector3(gameManager.GetStates()["health"], healthSlider.transform.localScale.y, healthSlider.transform.localScale.z);
        stressSlider.transform.localScale = new Vector3(gameManager.GetStates()["stress"], stressSlider.transform.localScale.y, stressSlider.transform.localScale.z);
        fatigueSlider.transform.localScale = new Vector3(gameManager.GetStates()["fatigue"], fatigueSlider.transform.localScale.y, fatigueSlider.transform.localScale.z);
    }
}

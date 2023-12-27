using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CreateSlider : MonoBehaviour
{
    [SerializeField] private GameObject slider;

    public GameObject CreateCurrentSlider(float position)
    {
        GameObject currentSlider = Instantiate(slider, new Vector2(0,position), Quaternion.identity);
        currentSlider.transform.SetParent(transform, false);
        return currentSlider;
    }
}

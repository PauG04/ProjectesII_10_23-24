using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;
    private CameraShake _camera;
    private float time = 0;

    [SerializeField] private DragHammer hammer;
    [SerializeField] private float IntensityShaking;

    private void Start()
    {
        m_BoxCollider= GetComponent<BoxCollider2D>();
        _camera = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        DetectClick();
        if(m_BoxCollider.enabled)
        {
            StartTimer();
        }
    }

    private void DetectClick()
    {
        if(Input.GetMouseButtonDown(1) && hammer.GetDragging()) 
        {
            m_BoxCollider.enabled = true;
            _camera.ShakeCamera(IntensityShaking);
            hammer.Animation(true);
        }
        else if(Input.GetMouseButtonUp(1)||!hammer.GetDragging())
        {
            m_BoxCollider.enabled = false;
        }
    }

    private void StartTimer()
    {
        time += Time.deltaTime;
        if(time >= 0.05)
        {
            m_BoxCollider.enabled = false;
            time = 0;
            _camera.SetTransforPosition();
            hammer.Animation(false);
        }
    }

}

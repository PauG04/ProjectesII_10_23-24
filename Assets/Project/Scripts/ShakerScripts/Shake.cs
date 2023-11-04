using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    [SerializeField] private float progress = 0.0f;

    Vector3 shakerPosition;
    Vector3 newShakerPosition;
    Vector3 oldShakerPosition;
    Vector3 shakerSize;

    [SerializeField] private bool shaking = false;
    [SerializeField] private float minimizeBarProgress;
    [SerializeField] private bool isShakingDown;

    private Drink shaker;
    private DrinkScript drink;

    [SerializeField] private GameObject[] sliders;
    [SerializeField] private SpriteRenderer[] ounce;

    private int currentBox = 0;
    private float maxValue = 2;
    private float value;

    [SerializeField]
    private float maxTime = 0;
    private float time = 0;

    [SerializeField] public CameraShake _camera;
    [SerializeField] private float shakeIntensity;
    private Quaternion starterRot;

    [SerializeField] private float maxSize;
    private int currentSprite = 0;

    private void Awake()
    {
        drink = GetComponent<DrinkScript>();
        shaker = GetComponent<Drink>();
        _camera = CameraShake.FindObjectOfType<CameraShake>();
        starterRot = transform.rotation;
        shakerSize = transform.localScale;
    }
    private void Start()
    {
        value = maxValue / 10;
        shakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        oldShakerPosition = shakerPosition;

        for (int i = 0; i < ounce.Length; i++)
        {
            ounce[i].enabled = true;
            ounce[i].color = Color.white;
        }
    }

    private void Update()
    {
        StartClicking();
        EndClicking();
        if (shaking && progress <= maxValue)
        {
            DirectionShaker();
            IncreaseBar();
            MoveShaker();
            SetShakerPosition();
            SetShakerStata();
            ActiveSlider();
        }
        else
        {
            _camera.SetTransforPosition();
            StopShaker();
        }
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            SetVector();
            time = 0;
        }

    }
    private void StartClicking()
    {
        if (!drink.GetIsMouseNotPressed() && shakerPosition != transform.position)
            shaking = true;
        if(!drink.GetIsMouseNotPressed()) 
        {
            transform.localScale = new Vector3(shakerSize.x + maxSize, shakerSize.y + maxSize, shakerSize.z);
        }
    }

    private void EndClicking()
    {
        if (Input.GetMouseButtonUp(0) || shakerPosition == transform.position)
        {
            shaking = false;
        }
        if(Input.GetMouseButtonUp(0))
        {
            transform.localScale = shakerSize;
        }
    }

    private void MoveShaker()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-shakeIntensity, shakeIntensity));
    }

    private void StopShaker()
    {
        transform.rotation = starterRot;
    }

    private void DirectionShaker()
    {
        newShakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (oldShakerPosition.y >= newShakerPosition.y)
        {
            isShakingDown = true;
        }
        else if (oldShakerPosition.y <= newShakerPosition.y)
        {
            isShakingDown = false;
        }

    }
    private void SetVector()
    {
        shakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void SetShakerPosition()
    {
        oldShakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void IncreaseBar()
    {
        if (isShakingDown)
            progress += (oldShakerPosition.y - newShakerPosition.y) / minimizeBarProgress;
        else
            progress += -(oldShakerPosition.y - newShakerPosition.y) / minimizeBarProgress;
        _camera.ShakeCamera((oldShakerPosition.y - newShakerPosition.y)*6);
    }

    private void ActiveSlider()
    {
        if (progress >= value)
        {
            sliders[currentBox].SetActive(true);
            currentBox++;
            value += maxValue / 10;
        }
    }

    private void SetShakerStata()
    {
        if (progress >= maxValue)
        {
            shaker.SetDrinkState(Drink.DrinkState.Shaked);
        }
        else if (progress > (maxValue / 2) && progress < maxValue)
        {
            shaker.SetDrinkState(Drink.DrinkState.Mixed);
        }

        else
        {
            shaker.SetDrinkState(Drink.DrinkState.Idle);
        }

    }
    public void ResetShaker()
    {
        progress = 0;
        shaker.SetDrinkState(Drink.DrinkState.Idle);
        value = maxValue / 10;
        currentBox = 0;
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].SetActive(false);
        }
        for (int i = 0; i < ounce.Length; i++)
        {
            ounce[i].color = Color.white;
        }
        currentSprite = 0;
    }

    public SpriteRenderer GetSprite()
    {
        return ounce[currentSprite];
    }

    public void SetIndex()
    {
        currentSprite++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    /*
    [SerializeField] private float progress = 0.0f;

    Vector3 shakerPosition;
    Vector3 newShakerPosition;
    Vector3 oldShakerPosition;
    Vector3 shakerSize;

    [SerializeField] private bool shaking = false;
    [SerializeField] private float minimizeBarProgress;
    [SerializeField] private bool isShakingDown;

    private LiquidManager shaker;
    private DrinkScript drink;

    [SerializeField] private GameObject[] sliders;
    private SpriteRenderer[] slidersSprite;
    [SerializeField] private GameObject[] ounce;
    [SerializeField] private SpriteRenderer[] ounceSpriter;

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

    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject shakeMesage;
    Vector3 arrowScale;
    Vector3 shakeScale;
    private bool showMesage;
    private bool justOneTime;
    private float timer = 0;

    [SerializeField] private float r;
    [SerializeField] private float g;
    [SerializeField] private float b;

    [SerializeField] CloseShaker close;

    private void Awake()
    {
        drink = GetComponent<DrinkScript>();
        shaker = GetComponent<LiquidManager>();
        _camera = CameraShake.FindObjectOfType<CameraShake>();
        starterRot = transform.rotation;
        shakerSize = transform.localScale;
    }
    private void Start()
    {
        value = maxValue / 10;
        shakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        oldShakerPosition = shakerPosition;

        for (int i = 0; i < ounceSpriter.Length; i++)
        {
            ounceSpriter[i].enabled = true;
            ounceSpriter[i].color = Color.white;

        }

        slidersSprite = new SpriteRenderer[10];

        for(int i = 0; i < sliders.Length; i++)
        {
            slidersSprite[i] = sliders[i].GetComponent<SpriteRenderer>();
            slidersSprite[i].color = new Color(255,255,255,255);
        }

        arrowScale = arrow.transform.localScale;
        shakeScale = shakeMesage.transform.localScale;
        arrow.transform.localScale = new Vector3(0, 0, 0);
        shakeMesage.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (close.GetClose())
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
            if (showMesage)
            {
                shakeMesage.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, shakeScale, 3 * Time.deltaTime);
                arrow.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, arrowScale, 3 * Time.deltaTime);

            }
            if (!showMesage)
            {
                shakeMesage.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
                arrow.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
            }
            if (shakeMesage.transform.localScale.x >= shakeScale.x - 0.2 && arrow.transform.localScale.x >= arrowScale.x - 0.2)
            {
                timer += Time.deltaTime;

                if (timer >= 2)
                {
                    showMesage = false;
                    timer = 0;
                }

            }
        }
        else
        {
            shakeMesage.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
            arrow.transform.localScale = Vector3.Lerp(shakeMesage.transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
        }

    }
    private void StartClicking()
    {
        if (!drink.GetIsMouseNotPressed() && shakerPosition != transform.position)
            shaking = true;
        if(!drink.GetIsMouseNotPressed()) 
        {
            transform.localScale = new Vector3(shakerSize.x + maxSize, shakerSize.y + maxSize, shakerSize.z);
            if(!justOneTime)
            {
                justOneTime = true;
                showMesage = true;
            }
        }
    }

    private void EndClicking()
    {
        if (Input.GetMouseButtonUp(0) || shakerPosition == transform.position)
        {
            shaking = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = shakerSize;
            showMesage = false;
            justOneTime = false;
        }
    }

    private void MoveShaker()
    {
        shakeIntensity = 1.5f+ ((oldShakerPosition.y - newShakerPosition.y) * 300.0f);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-shakeIntensity, shakeIntensity));
    }

    private void StopShaker()
    {
        transform.rotation = starterRot;
    }

    private void DirectionShaker()
    {
        newShakerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (oldShakerPosition.y >= newShakerPosition.y && isShakingDown == false)
        {
            isShakingDown = true;
        }
        else if (oldShakerPosition.y <= newShakerPosition.y && isShakingDown == true)
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
        _camera.ShakeCamera((oldShakerPosition.y - newShakerPosition.y)*1f);
    }

    private void ActiveSlider()
    {

        if (progress >= value)
        {
            sliders[currentBox].SetActive(true);
            slidersSprite[currentBox].color = new Color(r,g, b);
            g -= 0.1f;
            currentBox++;
            value += maxValue / 10;

            switch (Random.Range(1, 2))
            {
                case 1:
                    AudioManager.instance.Play("shake1");
                    break;
                case 2:
                    AudioManager.instance.Play("shake2");
                    break;
            }
        }
    }

    private void SetShakerStata()
    {
        if (progress >= maxValue)
        {
            shaker.SetDrinkState(LiquidManager.DrinkState.Shaked);
        }
        else if (progress > (maxValue / 2) && progress < maxValue)
        {
            shaker.SetDrinkState(LiquidManager.DrinkState.Mixed);
        }

        else
        {
            shaker.SetDrinkState(LiquidManager.DrinkState.Idle);
        }

    }
    public void ResetShaker()
    {
        progress = 0;
        shaker.SetDrinkState(LiquidManager.DrinkState.Idle);
        value = maxValue / 10;
        currentBox = 0;
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].SetActive(false);
            slidersSprite[i].color = new Color(255, 250, 250, 255);
        }
        for (int i = 0; i < ounceSpriter.Length; i++)
        {
            ounceSpriter[i].color = Color.white;
            ounce[i].SetActive(false);
        }
        currentSprite = 0;
        g = 1;
    }

    public SpriteRenderer GetSprite()
    {
        ounce[currentSprite].SetActive(true);
        return ounceSpriter[currentSprite];
    }

    public void SetIndex()
    {
        currentSprite++;
    }

    public float GetProgres()
    {
        return progress;
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsIconActivation : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject taskBar;
    [SerializeField] private GameObject buttonShutDown;
    [SerializeField] private GameObject buttonSetting;
    [SerializeField] private GameObject buttonReset;
    private BoxCollider2D shutdDownCollider;
    private BoxCollider2D SettingCollider;
    private BoxCollider2D ResetCollider;
    private SpriteRenderer reserSprite;
    private bool isOpen;
    private bool active;

    private void Awake()
    {
        ResetCollider = buttonReset.GetComponent<BoxCollider2D>();
        SettingCollider = buttonSetting.GetComponent<BoxCollider2D>();
        shutdDownCollider = buttonShutDown.GetComponent<BoxCollider2D>();
        reserSprite = buttonReset.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        isOpen = false;
        active = false;
        shutdDownCollider.enabled = false;
        SettingCollider.enabled = false;
        ResetCollider.enabled = false;
    }

    private void Update()
    {
        if (active)
        {
            if (!isOpen)
            {
                startMenu.transform.position = Vector3.Lerp(startMenu.transform.position, new Vector3(startMenu.transform.position.x, taskBar.transform.position.y + 0.1f, startMenu.transform.position.z), 4 * Time.deltaTime);
                if (startMenu.transform.position.y >= taskBar.transform.position.y + 0.098f)
                {
                    shutdDownCollider.enabled = true;
                    SettingCollider.enabled = true;
                    ResetCollider.enabled = true;
                    isOpen = true;
                    active = false;
                }

            }
            else
            {
                startMenu.transform.position = Vector3.Lerp(startMenu.transform.position, new Vector3(startMenu.transform.position.x, taskBar.transform.position.y - 0.01f, startMenu.transform.position.z), 4 * Time.deltaTime);
                shutdDownCollider.enabled = false;
                SettingCollider.enabled = false;
                ResetCollider.enabled = false;
                if (startMenu.transform.position.y <= taskBar.transform.position.y)
                {

                    isOpen = false;
                    active = false;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        active = true;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

}

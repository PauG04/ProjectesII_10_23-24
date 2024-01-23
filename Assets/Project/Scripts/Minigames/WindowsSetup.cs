using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsSetup : MonoBehaviour
{
	private WindowsStateMachine window;
    [SerializeField] private List<DesktopApp> childApps = new List<DesktopApp>();

    private void Start()
	{
        window = gameObject.transform.parent.GetComponent<WindowsStateMachine>();

		if (window != null)
		{
            window.SetChildApps(childApps);
        }
    }

    public WindowsStateMachine GetWindows()
    {
        return window;
    }
}

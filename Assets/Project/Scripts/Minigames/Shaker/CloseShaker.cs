using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShaker : MonoBehaviour
{

	[SerializeField] private ShakerController shakerController;
	private bool close;

    private void OnMouseDown()
    {
        close = !close;
        shakerController.SetAnimation(!close);
    }

    public bool GetClose()
    {
        return close;
    }
}

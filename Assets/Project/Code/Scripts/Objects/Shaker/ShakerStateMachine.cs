using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerStateMachine : StateMachineManager<ShakerStateMachine.ShakerState>
{

    [Header("General Shaker Variables")]
    [SerializeField] private SetTopShaker topShaker;
    [SerializeField] private GameObject bottomShaker;
    [SerializeField] private LayerMask shakerLayerMask;

    [Header("Progress Variables")]
    [SerializeField] private float maxProgress;
	[SerializeField] private float divideProgress;
	private bool isPressing;
	private float progress;

	[Header("Liquid Variables")]
	[SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;

	private ShakerDraggingClose shakerDraggingClose;
	private ShakerDraggingOpen shakerDraggingOpen;

	[Header("WorkSpace Variables")]
	[SerializeField] private Sprite workspaceSprite;
    [SerializeField] private Sprite initSprite;
	[SerializeField] private Collider2D workSpace;
    private bool isInWorkSpace;

	[Header("Progress Slider")]
	//[SerializeField] private ProgressSlider slider;
    [SerializeField] private Slider progressSlider;
	[SerializeField] private Image color;
	[SerializeField] private Image background;

	private Vector3 initPosition;

    public enum ShakerState
	{
		IdleOpen,
		IdleClosed,
		DraggingOpen,
		DraggingClosed,
		ResetDrink
	}

	private void Awake()
	{
		isInWorkSpace = false;

        initPosition = transform.localPosition;

        shakerDraggingClose = new ShakerDraggingClose(this, progress, maxProgress, divideProgress, workSpace, progressSlider, color, background);
		shakerDraggingOpen = new ShakerDraggingOpen(this, liquidPref, spawnPoint, liquidManager, workSpace, color, background);

        States.Add(ShakerState.IdleOpen, new ShakerIdleOpen(this, topShaker, initPosition, workSpace, color, background));
		States.Add(ShakerState.IdleClosed, new ShakerIdleClose(this, topShaker, shakerLayerMask, initPosition, workSpace, color, background));
		States.Add(ShakerState.DraggingOpen, shakerDraggingOpen);
		States.Add(ShakerState.DraggingClosed, shakerDraggingClose);
		States.Add(ShakerState.ResetDrink, new ShakerResetDrink());

		CurrentState = States[ShakerState.IdleOpen];

		progressSlider.value = 0;
		progressSlider.maxValue = maxProgress;
		color.color = new Color(1, 1, 0, 0);
        background.color = new Color(1, 1, 1, 0);

    }
    public void ChangingState()
	{
		Debug.Log(IsTranistioningState);
	}
    public void SetProgress(float progress)
	{
		this.progress = progress;
	}

	public void SetGetInWorkSpace(bool isInWorkSpace)
	{
		this.isInWorkSpace = isInWorkSpace;
	}

	public bool GetIsInWorkSpace()
	{
		return isInWorkSpace;
	}

    public float GetProgress()
	{
		return shakerDraggingClose.GetProgress();
	}
	public float GetMaxProgress()
	{
		return maxProgress;
	}
	public bool GetIsMousePressed()
	{
		return isPressing;
	}

}
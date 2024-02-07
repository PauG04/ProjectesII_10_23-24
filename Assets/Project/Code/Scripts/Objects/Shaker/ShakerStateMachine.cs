using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	[Header("Drag Shaker")]
	[SerializeField] private float maxAngle;

	[Header("Open Shaker Rotation")]
	[SerializeField] private float rotationSpeed;

	[Header("Liquid Variables")]
	[SerializeField] private GameObject liquidPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LiquidManager liquidManager;

	private ShakerDraggingClose shakerDraggingClose;
	private ShakerDraggingOpen shakerDraggingOpen;

	[Header("WorkSpace Variables")]
	private bool isInWorkSpace;

	[Header("Shaker Top")]
	[SerializeField] private LerpTopShaker lerpTopShaker;

	[Header("Progress Slider")]
	[SerializeField] private ProgressSlider slider;

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

        shakerDraggingClose = new ShakerDraggingClose(this, maxAngle, progress, maxProgress, divideProgress, slider);
		shakerDraggingOpen = new ShakerDraggingOpen(this, rotationSpeed, liquidPref, spawnPoint, liquidManager);

        States.Add(ShakerState.IdleOpen, new ShakerIdleOpen(this, topShaker, initPosition));
		States.Add(ShakerState.IdleClosed, new ShakerIdleClose(this, topShaker, shakerLayerMask, lerpTopShaker, initPosition, slider));
		States.Add(ShakerState.DraggingOpen, shakerDraggingOpen);
		States.Add(ShakerState.DraggingClosed, shakerDraggingClose);
		States.Add(ShakerState.ResetDrink, new ShakerResetDrink());

		CurrentState = States[ShakerState.IdleOpen];
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
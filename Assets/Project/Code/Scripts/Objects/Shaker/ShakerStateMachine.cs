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
	[SerializeField] private GameObject _liquidPref;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LiquidManager _liquidManager;

	private ShakerDraggingClose shakerDraggingClose;
	private ShakerDraggingOpen shakerDraggingOpen;

	[Header("WorkSpace Variables")]
	[SerializeField] private float increaseScale;
	private Vector2 initScale;
	private bool isInWorkSpace;

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
		initScale = transform.localScale;

        shakerDraggingClose = new ShakerDraggingClose(this, maxAngle, progress, maxProgress, divideProgress, increaseScale, isInWorkSpace, initScale);
		shakerDraggingOpen = new ShakerDraggingOpen(this, rotationSpeed, _liquidPref, _spawnPoint, _liquidManager, increaseScale, isInWorkSpace, initScale);

        States.Add(ShakerState.IdleOpen, new ShakerIdleOpen(this, topShaker));
		States.Add(ShakerState.IdleClosed, new ShakerIdleClose(this, topShaker, shakerLayerMask));
		States.Add(ShakerState.DraggingOpen, shakerDraggingOpen);
		States.Add(ShakerState.DraggingClosed, shakerDraggingClose);
		States.Add(ShakerState.ResetDrink, new ShakerResetDrink());

		CurrentState = States[ShakerState.IdleOpen];
	}
    public void SetProgress(float progress)
	{
		this.progress = progress;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerStateMachine : MonoBehaviour
{
	[Header("Shaker General Variables")]
	[SerializeField] private CameraShake cameraShake;
	[SerializeField] private WindowsSetup windowSetup;
	[SerializeField] private CloseShaker close;
	[SerializeField] private GameObject shakerTop;
	[SerializeField] private float divideProgress;
	[SerializeField] private float maxProgress;
	[SerializeField] private float intensityShaking;

	private Rigidbody2D rb;
	private Vector2 newPosition;
	private Transform parent;

	private bool isPressing;
	private bool isDown;
	private bool canShake;
	private float progress;
	
	[Header("Drag Shaker")]
	[SerializeField] private float maxAngle;
	[SerializeField] private bool hasToRotate;
	private TargetJoint2D targetJoint;
	private Vector2 position;
	private Vector3 offset;
	private bool draggingClosedShaker = false;
	private bool draggingOpenShaker = false;
	
	[Header("Shaker Rotation Variables")]
	[SerializeField] private GameObject rotateTowards;
	[SerializeField] private float rotationVelocity;
	private Vector2 objectPosition;
	private bool isRotating = false;
	
	[Header("Fluid Simulation Variables")]
	[SerializeField] private LiquidManager liquidManager;
	[SerializeField] private GameObject liquidParticle;
	[SerializeField] private float spawnRate;
	[Space(20)]
	[SerializeField] private GameObject simulation;
	[SerializeField] private Renderer filterRenderer;
	[SerializeField] private Color liquidColor;
	private float time;
	
	public enum ShakerState
	{
		IdleOpen,
		IdleClosed,
		OpenShaker,
		CloseShaker,
		DraggingOpen,
		DraggingClosed,
		ResetDrink,
		ResetPositions
	}
	
	private void Awake()
	{
		/*
		States.Add(ShakerState.IdleOpen, new ShakerIdleOpen(this, liquidManager));
		States.Add(ShakerState.IdleClosed, new ShakerIdleClose(this));
		States.Add(ShakerState.OpenShaker, new ShakerOpenAnimation());
		States.Add(ShakerState.CloseShaker, new ShakerCloseAnimation());
		States.Add(ShakerState.DraggingOpen, new ShakerDraggingOpen());
		States.Add(ShakerState.DraggingClosed, new ShakerDraggingClose());
		States.Add(ShakerState.ResetDrink, new ShakerResetDrink());
		States.Add(ShakerState.ResetPositions, new ShakerResetPosition());
		*/
	}
	
	public void SetProgress(float progress)
	{
		this.progress = progress;
	}
	
	public float GetProgress()
	{
		return progress;
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

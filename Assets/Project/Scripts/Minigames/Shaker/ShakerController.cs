using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ShakerController : MonoBehaviour
{
	#region Shaker General Variables
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
    #endregion
    #region Shaker Drag Variables
	[Header("Drag Shaker")]
	[SerializeField] private float maxAngle;
	[SerializeField] private bool hasToRotate;

    private TargetJoint2D targetJoint;
    private Vector2 position;
    private Vector3 offset;
    private bool draggingClosedShaker = false;
	private bool draggingOpenShaker = false;
    #endregion
    #region Shaker Rotation Variables
    [Header("Shaker Rotation Variables")]
	[SerializeField] private GameObject rotateTowards;
	[SerializeField] private float rotationVelocity;

    private Vector2 objectPosition;
    private bool isRotating = false;
    #endregion
    #region Fluid Simulation Variables
    [Header("Fluid Simulation Variables")]
    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private GameObject liquidParticle;
	[SerializeField] private float spawnRate;
	[Space(20)]
	[SerializeField] private GameObject simulation;
	[SerializeField] private Renderer filterRenderer;
	[SerializeField] private Color liquidColor;

	private float time;
	#endregion
	
	#region Unity Functions
	private void Awake()
	{
		cameraShake = Camera.main.GetComponent<CameraShake>();
		rb = GetComponent<Rigidbody2D>();
		targetJoint= GetComponent<TargetJoint2D>();
		parent = transform.parent.GetComponent<Transform>();
		filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
		simulation = GameObject.Find("Simulation");

		canShake = false;
        newPosition = transform.position;
	}
	private void Update()
	{
		if (!close.GetClose())
		{
            shakerTop.SetActive(false);
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;

            if (draggingOpenShaker)
            {
                DragShakerIfOpen();
            }
            if (!isRotating)
            {
				ResetRotation();
            }
        }
		else
		{
			shakerTop.SetActive(true);
            if (draggingClosedShaker)
            {
                Shaking();
                DragShakerIfClosed();
            }
        }

		if (!isPressing)
		{
            liquidManager.GetComponent<BoxCollider2D>().enabled = true;
            ResetShakerTransform();
		}
    }
    private void OnMouseDown()
    {
        if (close.GetClose())
        {
            draggingClosedShaker = true;
        }
        else
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggingOpenShaker = true;
        }
        isPressing = true;
    }
    private void OnMouseUp()
	{
		draggingClosedShaker = false;
		draggingOpenShaker = false;
        isPressing = false;
	}
	#endregion
	#region Dragging Functions
	private void DragShakerIfClosed()
	{
		CalculatePositionClosed();
        transform.SetParent(null);

        if (hasToRotate)
		{
			rb.SetRotation(Vector2.Dot(rb.velocity.normalized, Vector2.up) * rb.velocity.sqrMagnitude * maxAngle);
		}
		
		if (windowSetup.GetWindows().GetCurrentState() == WindowsStateMachine.WindowState.Dragging)
		{
			rb.bodyType = RigidbodyType2D.Kinematic;
		}
		else
		{
			rb.constraints = RigidbodyConstraints2D.None;
			rb.bodyType = RigidbodyType2D.Dynamic;
		}
	}
    private void DragShakerIfOpen()
    {
        CalculatePositionOpen();
		transform.SetParent(null);

		if(isRotating)
		{
            RotateObjectTowards();
        }

        if (Input.GetMouseButtonDown(1) && rotateTowards != null)
        {
            objectPosition = rotateTowards.transform.position;
			isRotating = true;
		}
        if (Input.GetMouseButtonUp(1) && rotateTowards != null)
        {
            objectPosition = Vector2.zero;
			isRotating = false;
        }
        if (transform.up.y < 0)
        {
            liquidManager.GetComponent<BoxCollider2D>().enabled = false;
            float spawnRateLiquid = (transform.up.y * spawnRate) / -1;
            DropLiquid(spawnRateLiquid);
        }
    }
    private void CalculatePositionOpen()
    {
        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offset.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offset.y,
            0
        );
    }
    private void CalculatePositionClosed()
	{
		if (draggingClosedShaker)
		{
			targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else
		{
			targetJoint.target = (Vector2)windowSetup.GetWindows().transform.localPosition + position;
		}
	}
	private void RotateObjectTowards()
	{
		Vector3 dir = (Vector3)objectPosition - transform.position;
		
		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		
		Quaternion objectiveRotation = Quaternion.Euler(0, 0, -angle);
		transform.rotation = Quaternion.Lerp(transform.rotation, objectiveRotation, rotationVelocity * Time.deltaTime);
	}
	private void ResetRotation()
	{
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationVelocity * Time.deltaTime);
    }
    private void ResetShakerTransform()
	{
		transform.SetParent(parent);
		transform.localPosition = Vector3.zero;
	}
	#endregion
	#region Fluid Functions
	private void DropLiquid(float spawnRateLiquid)
	{
		if (liquidManager.GetCurrentLiquid() > 0)
		{
			time += Time.deltaTime;

			if (time < 1.0f / spawnRateLiquid)
			{
				return;
			}
			// TODO: The color changes the material, but doing that changes all the other particles colors, find other method
			filterRenderer.material.SetColor("_Color", liquidColor);

			GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
			newParticle.transform.parent = simulation.transform;
			newParticle.transform.position = transform.position;
			time = 0.0f;
			liquidManager.DecreaseLiquid();
		}
	}
	#endregion
	#region Shaking Functions
	private void Shaking()
	{
		StartShaking();
		EndClicking();
		if (canShake && progress <= maxProgress)
		{
			DirectionShaker();
			IncreaseBar();
		}
		else
		{
			cameraShake.SetTransforPosition();
		}
		SetDrinkState();
	}
	private void StartShaking()
	{
		if ((rb.velocity.y >= 0.00001f || rb.velocity.y <= -0.00001f) && isPressing)
		{
			canShake = true;
		}   
	}
	private void EndClicking()
	{
		if((rb.velocity.y <= 0.00001f && rb.velocity.y >= -0.00001f) || progress >= maxProgress)
		{
			canShake = false;
		}
	}
	private void DirectionShaker()
	{
		isDown = !(isDown && rb.velocity.y >= 0 && canShake);
	}
	private void IncreaseBar()
	{
		if(isDown)
		{
			progress += (newPosition.y - transform.position.y) / divideProgress;
		}
		else
		{
			progress += (transform.position.y - newPosition.y) / divideProgress;
		}
		cameraShake.ShakeCamera((transform.position.y - newPosition.y) * intensityShaking);
		newPosition = transform.position;
	}
	#endregion
	#region Shaker State
	private void SetDrinkState()
	{
		if(progress < maxProgress * 0.33f)
		{
			liquidManager.SetDrinkState(LiquidManager.DrinkState.Idle);
		}
		else if(progress >= maxProgress * 0.66f)
		{
			liquidManager.SetDrinkState(LiquidManager.DrinkState.Mixed);
		}
		else
		{
			liquidManager.SetDrinkState(LiquidManager.DrinkState.Shaked);
		}
	}
	public void ResetShaker()
	{
		progress = 0;
		liquidManager.SetDrinkState(LiquidManager.DrinkState.Idle);
	}
	#endregion
	#region Setters and Getters 
	public float GetProgess()
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
	#endregion
}
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
	[SerializeField] private float divideProgress;
	[SerializeField] private float maxProgress;
	[SerializeField] private float intensityShaking;

	private LiquidManager liquidManager;
	private Rigidbody2D rb;
	private Animator anim;
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

	private bool draggingClosedShaker = false;
	private bool draggingOpenShaker = false;
	private TargetJoint2D targetJoint;
	private Vector2 position;
    private Vector3 offset;
    #endregion
    #region Shaker Rotation Variables
    [Header("Shaker Rotation Variables")]
	[SerializeField] private GameObject rotateTowards;
	[SerializeField] private float rotationVelocity;

    private bool isRotating = false;
    private Vector2 objectPosition;
    #endregion
    #region Fluid Simulation Variables
    [Header("Fluid Simulation Variables")]
	[SerializeField] private GameObject liquidParticle;
	[SerializeField] private float spawnRate;
	[SerializeField] private int maxQuantityOfLiquid;
	[Space(20)]
	[SerializeField] private GameObject simulation;
	[SerializeField] private Renderer filterRenderer;
	[SerializeField] private Color liquidColor;
	public LiquidManager.TypeOfDrink drinksType;

	private int quantityOfLiquid;
	private float time;
	#endregion
	
	#region Unity Functions
	private void Awake()
	{
		cameraShake = Camera.main.GetComponent<CameraShake>();
		rb = GetComponent<Rigidbody2D>();
		targetJoint= GetComponent<TargetJoint2D>();
		liquidManager = GetComponent<LiquidManager>();
		parent = transform.parent.GetComponent<Transform>();
		filterRenderer = GameObject.FindGameObjectWithTag("FluidTextureCamera").GetComponent<Renderer>();
		simulation = GameObject.Find("Simulation");
		anim = GetComponent<Animator>();

		canShake = false;
		newPosition = transform.position;
	}
	private void Update()
	{
		if (draggingClosedShaker)
		{
            Shaking();
			DragShakerIfClosed();
		}

		if (!close.GetClose())
		{
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (draggingOpenShaker)
            {
                DragShakerIfOpen();
            }
        }
        
        if (!isPressing || !isRotating)
        {
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
			rb.bodyType = RigidbodyType2D.Dynamic;
		}
	}
    private void DragShakerIfOpen()
    {
        CalculatePositionOpen();

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
	private void ResetShakerTransform()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationVelocity * Time.deltaTime);
	}
	#endregion
	#region Fluid Functions
	private void DropLiquid(float spawnRateLiquid)
	{
		if (quantityOfLiquid > 0)
		{
			time += Time.deltaTime;

			if (time < 1.0f / spawnRateLiquid)
			{
				return;
			}
			// TODO: The color changes the material, but doing that changes all the other particles colors, find other method
			filterRenderer.material.SetColor("_Color", liquidColor);
			liquidParticle.GetComponent<LiquidParticle>().liquidType = drinksType;

			GameObject newParticle = Instantiate(liquidParticle, transform.position, Quaternion.identity);
			newParticle.transform.parent = simulation.transform;
			newParticle.transform.position = transform.position;
			time = 0.0f;
			quantityOfLiquid--;
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
		//SetDrinkState();
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
	public void SetAnimation(bool value)
	{
		anim.SetBool("isOpen", value);
	}
	#endregion
}
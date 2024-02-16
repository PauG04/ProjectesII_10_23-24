using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Client : MonoBehaviour
{
    private CocktailNode.Type order;
    private float payment;

    private SpriteRenderer spriteRenderer;

    [Header("Dialogue")]
    [SerializeField] private GameObject dialogue;
    private TextMeshPro textMP;

    [Header("Client Position")]
    [SerializeField] private GameObject clientPosition;
    [SerializeField] private GameObject leavePosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float verticalVelocity;

    private  float minYPosition;
    private bool isGoingUp;

    private bool arriveAnimation;
    private bool leaveAnimation;

    [SerializeField] private float maxTime;
    private float time;
    private bool startTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMP = GetComponentInChildren<TextMeshPro>();

        arriveAnimation = false;
        leaveAnimation = false;
        isGoingUp = true;
        minYPosition = transform.localPosition.y;

        dialogue.SetActive(false);
        startTimer = false;
    }

    private void Start()
    {
        ArriveAnimation();
    }

    #region INIT
    private void InitOrder()
    {
        //int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
        //order = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
        order = CocktailNode.Type.Invade;
        textMP.text = "Quiero un " + order.ToString();
    }

    private void InitPayment()
    {
        payment = Random.Range(10.0f, 100.0f);
    }

    private void InitClient()
    {
        InitOrder();
        InitPayment();
        dialogue.SetActive(true);
    }
    #endregion

    private bool CompareCocktails(CocktailNode.Type cocktail)
    {
        if (cocktail == order)
            return true;
        return false;
    }
     
    public void ReceiveCoctel(CocktailNode.Type cocktail)
    {
        if (CompareCocktails(cocktail))
        {
            ReactWell();
            return;
        }

        ReactBad();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cocktail") && CursorManager.instance.IsMouseUp())
        {
            Debug.Log(collision.GetComponentInChildren<LiquidManager>().GetDrinkState());

            ReceiveCoctel(CalculateDrink.instance.CalculateResultDrink(
                collision.GetComponentInChildren<LiquidManager>().GetParticleTypes(),
                collision.GetComponentInChildren<LiquidManager>().GetDrinkState()
                ));
            startTimer = true;
        }
    }

    private void ReactWell()
    {
        textMP.text = "VIVA FRANCO";
        //Change Animation
        Pay();
        //Wait X Seconds
        //ClientManager.instance.CreateNewClient();
    }

    private void ReactBad()
    {
        textMP.text = "Menudo MIERDON";
        //Change Animation
        //Wait X Seconds
        //ClientManager.instance.CreateNewClient();
    }

    private void Pay()
    {
        EconomyManager.instance.AddMoney(payment);
    }

    private void ArriveAnimation()
    {
        arriveAnimation = true;
    }

    private void Update()
    {
        if(arriveAnimation)
        {
            MoveClientHorizontal(clientPosition);
            MoveClientVertical();
            if (transform.localPosition.x > clientPosition.transform.localPosition.x - 0.01 && transform.localPosition.y < minYPosition + 0.1) 
            {
                arriveAnimation= false;
                InitClient();
            }
        }

        if (startTimer)
        {
            time += Time.deltaTime;
            if(time > maxTime)
            {
                leaveAnimation = true;
                dialogue.SetActive(false);
            }
        }

        if(leaveAnimation)
        {
            MoveClientHorizontal(leavePosition);
            MoveClientVertical();
            if (transform.localPosition.x > leavePosition.transform.localPosition.x - 0.01 && transform.localPosition.y < minYPosition + 0.1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveClientHorizontal(GameObject _gameObject)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x = Mathf.Lerp(transform.localPosition.x, _gameObject.transform.localPosition.x, Time.deltaTime * horizontalVelocity);

        transform.localPosition = newPosition;
    }

    private void MoveClientVertical() 
    {
        if (isGoingUp)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, maxYPosition, Time.deltaTime * verticalVelocity);

            transform.localPosition = newPosition;

            if (transform.localPosition.y > maxYPosition - 0.01)
            {
                isGoingUp = false;
            }
        }
        else
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, minYPosition, Time.deltaTime * verticalVelocity);

            transform.localPosition = newPosition;

            if (transform.localPosition.y < minYPosition + 0.01)
            {
                isGoingUp = true;
            }
        }
    }
}

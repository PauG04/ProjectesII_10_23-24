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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMP = GetComponentInChildren<TextMeshPro>();

        arriveAnimation = false;
        leaveAnimation = false;
        isGoingUp = true;
        minYPosition = transform.position.y;

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
             ReceiveCoctel(collision.gameObject.GetComponent<Cocktail>().GetCocktail().type);
            leaveAnimation = true;
        }
    }

    private void ReactWell()
    {
        //Change Animation
        Pay();
        //Wait X Seconds
        ClientManager.instance.CreateNewClient();
    }

    private void ReactBad()
    {
        //Change Animation
        //Wait X Seconds
        ClientManager.instance.CreateNewClient();
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
            if (transform.position.x > clientPosition.transform.position.x - 0.01 && transform.position.y < minYPosition + 0.1) 
            {
                arriveAnimation= false;
                InitClient();
            }
        }
        if(leaveAnimation)
        {
            MoveClientHorizontal(leavePosition);
            MoveClientVertical();
            if (transform.position.x > leavePosition.transform.position.x - 0.01 && transform.position.y < minYPosition + 0.1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveClientHorizontal(GameObject _gameObject)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, _gameObject.transform.position.x, Time.deltaTime * horizontalVelocity);

        transform.position = newPosition;
    }

    private void MoveClientVertical() 
    {
        if (isGoingUp)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Lerp(transform.position.y, maxYPosition, Time.deltaTime * verticalVelocity);

            transform.position = newPosition;

            if (transform.position.y > maxYPosition - 0.01)
            {
                isGoingUp = false;
            }
        }
        else
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Lerp(transform.position.y, minYPosition, Time.deltaTime * verticalVelocity);

            transform.position = newPosition;

            if (transform.position.y < minYPosition + 0.01)
            {
                isGoingUp = true;
            }
        }
    }
}

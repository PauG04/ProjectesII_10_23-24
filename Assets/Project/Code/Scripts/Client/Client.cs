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

    private float minYPosition;
    private bool isGoingUp;

    private bool arriveAnimation;
    private bool leaveAnimation;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        arriveAnimation = false;
        leaveAnimation = false;

        isGoingUp = true;
        minYPosition = transform.localPosition.y;
    }

    private void Start()
    {
        transform.localPosition = ClientManager.instance.GetSpawnPosition().localPosition;
        ArriveAnimation();
    }

    private void InitClient()
    {
        //int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
        //order = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
        //payment = WikiManager.instance.GetAvailableCocktails()[randomOrder].price;

        order = CocktailNode.Type.Mojito;
        payment = 10.0f;
    }

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
            leaveAnimation = true;
        }
    }

    private void ReactWell()
    {
        //textMP.text = "VIVA FRANCO";
        //Change Animation
        Pay();
        //Wait X Seconds
        //ClientManager.instance.CreateNewClient();
    }

    private void ReactBad()
    {
        //textMP.text = "Menudo MIERDON";
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
        if (arriveAnimation)
        {
            MoveClientHorizontal(ClientManager.instance.GetClientPosition());
            MoveClientVertical();
            if (transform.localPosition.x > ClientManager.instance.GetClientPosition().localPosition.x - 0.01 && transform.localPosition.y < minYPosition + 0.1)
            {
                arriveAnimation = false;
                InitClient();
            }
        }

        if (leaveAnimation)
        {
            MoveClientHorizontal(ClientManager.instance.GetLeavePosition());
            MoveClientVertical();
            if (transform.localPosition.x > ClientManager.instance.GetLeavePosition().localPosition.x - 0.01 && transform.localPosition.y < minYPosition + 0.1)
            {
                ClientManager.instance.CreateNewClient();
                Destroy(gameObject);
            }
        }
    }

    private void MoveClientHorizontal(Transform _transform)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x = Mathf.Lerp(transform.localPosition.x, _transform.localPosition.x, Time.deltaTime * ClientManager.instance.GetHorizontalVelocity());

        transform.localPosition = newPosition;
    }

    private void MoveClientVertical()
    {
        if (isGoingUp)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, ClientManager.instance.GetMaxYPosition(), Time.deltaTime * ClientManager.instance.GetVerticalVelocity());

            transform.localPosition = newPosition;

            if (transform.localPosition.y > ClientManager.instance.GetMaxYPosition() - 0.01)
            {
                isGoingUp = false;
            }
        }
        else
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, minYPosition, Time.deltaTime * ClientManager.instance.GetVerticalVelocity());

            transform.localPosition = newPosition;

            if (transform.localPosition.y < minYPosition + 0.01)
            {
                isGoingUp = true;
            }
        }
    }

    public CocktailNode.Type GetOrder()
    {
        return order;
    }
    
}
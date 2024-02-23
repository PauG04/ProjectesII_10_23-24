using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Client : MonoBehaviour
{
    private CocktailNode.Type drink;
    private float payment;

    private SpriteRenderer spriteRenderer;

    [Header("Client Position")]
    [SerializeField] private GameObject clientPosition;
    [SerializeField] private GameObject leavePosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float verticalVelocity;
    private BoxCollider2D boxCollider;

    [Header("Booleans")]
    [SerializeField] private bool notNeedTakeDrink;
    private bool canLeave;

    [SerializeField] private float minYPosition;
    private bool isGoingUp;

    private bool arriveAnimation;
    private bool leaveAnimation;
    private bool startTimer;

    [Header("Client Dialogue")]
    private AIConversant conversant;

    [Header("Timer")]
    [SerializeField] private float maxTime;
    private float time;

    private bool isTutorial;

    private void Awake()
    {
        conversant = GetComponent<AIConversant>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.enabled = false;
        arriveAnimation = false;
        leaveAnimation = false;
        startTimer = false;
        time = 0;

        isGoingUp = true;

        canLeave = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        isTutorial = false;
    }

    private void Start()
    {
        transform.localPosition = ClientManager.instance.GetSpawnPosition().localPosition;
        ArriveAnimation();
    }

    private void InitClient()
    {
        boxCollider.enabled = true;
        // Hard coded, change later


        //int randomDialogue = Random.Range(0, ClientManager.instance.GetRegularClientDialogues().Count);
        //conversant.SetDialogue(ClientManager.instance.GetRegularClientDialogues()[randomDialogue]);
        /// <summary>
        /// TODO: Dani
        ///     Hacer que el dialogo reciba la señal dependiendo del tipo de randomOrder, haciendo que escoja el dialogo dependiendo de la bebida
        /// </summary>


        if (isTutorial)
        {
            drink = CocktailNode.Type.Roncola;
            conversant.HandleDialogue();
        }
        else
        {
            int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
            drink = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
            Dialogue.Dialogue currentDialogue = ClientManager.instance.GetRegularClientDialogues()[randomOrder];
            conversant.SetDialogue(currentDialogue);
            conversant.HandleDialogue();
            Debug.Log(drink);
        }
        
        payment = 10.0f;
    }

    private bool CompareCocktails(CocktailNode.Type cocktail)
    {
        if (cocktail == drink)
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

            Destroy(collision.gameObject);
        }
    }

    private void ReactWell()
    {
        //textMP.text = "VIVA FRANCO";
        //Change Animation
        conversant.SetDialogue(ClientManager.instance.GetGoodReactionDialogue());
        conversant.HandleDialogue();
        Pay();
        //Wait X Seconds
        //ClientManager.instance.CreateNewClient();
    }

    private void ReactBad()
    {
        //textMP.text = "Menudo MIERDON";
        //Change Animation
        conversant.SetDialogue(ClientManager.instance.GetBadReactionDialogue());
        conversant.HandleDialogue();
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
            boxCollider.enabled = false;
             
            MoveClientHorizontal(ClientManager.instance.GetLeavePosition());
            MoveClientVertical();
            if (transform.localPosition.x > ClientManager.instance.GetLeavePosition().localPosition.x - 0.01 && transform.localPosition.y < minYPosition + 0.1)
            {
                ClientManager.instance.CreateNewClient();
                Destroy(gameObject);
            }
        }

        if(startTimer)
        {
            Timer();
        }

        if(notNeedTakeDrink && canLeave)
        {
            startTimer = true;
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

    public void Timer()
    {
        time += Time.deltaTime;
        if(time > maxTime)
        {
            startTimer = false;
            leaveAnimation = true;
        }
    }

    public CocktailNode.Type GetOrder()
    {
        return drink;
    }
    public void SetOrder(CocktailNode.Type drink)
    {
        this.drink = drink;
    }
    public void SetCanLeave(bool state)
    {
        canLeave = state;
    }

    public void SetNotNeedTakeDrink(bool value)
    {
        notNeedTakeDrink = value;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public AIConversant GetConversant()
    {
        return conversant;
    }

    public void SetIsTutorial(bool state)
    {
        isTutorial = state;
    }
}
using Dialogue;
using UnityEngine;

public class Client : MonoBehaviour
{
    private CocktailNode order;
    private float payment;

    private SpriteRenderer spriteRenderer;

    [Header("Client Position")]
    [SerializeField] private GameObject clientPosition;
    [SerializeField] private GameObject leavePosition;
    [SerializeField] private float horizontalVelocity;
    private BoxCollider2D boxCollider;

    [Header("Booleans")]
    private bool canLeave;

    private bool arriveAnimation;
    private bool leaveAnimation;
    private bool startTimer;

    [Header("Client Dialogue")]
    private AIConversant conversant;

    [Header("Timer")]
    [SerializeField] private float maxTime;
    private float time;

    private bool isLocated;

    private ClientNode clientNode;

    private bool hitted;

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

        canLeave = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        isLocated = false;
        hitted = false;
    }

    private void Start()
    {
        transform.localPosition = ClientManager.instance.GetSpawnPosition().localPosition;
        ArriveAnimation();
    }

    private void Update()
    {
        if (arriveAnimation)
        {
            MoveClientHorizontal(ClientManager.instance.GetClientPosition());
            if (transform.localPosition.x > ClientManager.instance.GetClientPosition().localPosition.x - 0.01)
            {
                arriveAnimation = false;
                isLocated = true;
                conversant.HandleDialogue();
            }
        }
        else if (leaveAnimation)
        {
            boxCollider.enabled = false;

            MoveClientHorizontal(ClientManager.instance.GetLeavePosition());
            if (transform.localPosition.x > ClientManager.instance.GetLeavePosition().localPosition.x - 0.01)
            {
                ClientManager.instance.CreateClient();
                Destroy(gameObject);
            }
        }

        if (canLeave && clientNode.notNeedTakeDrink)
        {
            startTimer = true;
        }

        if (startTimer)
        {
            Timer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cocktail") && CursorManager.instance.IsMouseUp() && !clientNode.notNeedTakeDrink)
        {
            LiquidManager liquidManagerResult = collision.GetComponentInChildren<LiquidManager>();

            ReceiveCoctel(CalculateDrink.instance.CalculateResultDrink(
                liquidManagerResult.GetParticleTypes(),
                liquidManagerResult.GetDrinkState(),
                collision.GetComponentInChildren<SpriteRenderer>().sprite,
                collision.GetComponentInChildren<InsideDecorations>().GetDecorations()
                ));

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer") && !startTimer && clientNode.canBeHitted)
        {
            hitted = true;
            if (clientNode.hitToGo)
            {
                conversant.SetDialogue(clientNode.goodReaction);
                conversant.HandleDialogue();
                return;
            }

            int randomHitDialogue = Random.Range(0, ClientManager.instance.GetRegularClientHitDialogues().Count);
            conversant.SetDialogue(ClientManager.instance.GetRegularClientHitDialogues()[randomHitDialogue]);
            conversant.HandleDialogue();

            
        }
    }

    public void InitClient()
    {
        boxCollider.enabled = true;

        int randomOrder = Random.Range(0, clientNode.possibleOrders.Count);
        order = clientNode.possibleOrders[randomOrder];
        payment = order.price;

        Dialogue.Dialogue currentDialogue = clientNode.dialogues[randomOrder];
        conversant.SetDialogue(currentDialogue);

        spriteRenderer.sprite = clientNode.sprite;
        spriteRenderer.flipX = true;
    }

    private bool CompareCocktails(CocktailNode.Type cocktail)
    {
        if (clientNode.acceptsAll == true)
            return true;
        if (order.type == cocktail)
            return true;
        return false;
    }

    public void ReceiveCoctel(CocktailNode.Type cocktail)
    {
        if (CompareCocktails(cocktail))
        {
            Debug.Log("AQUI SI");
            ReactWell();
            return;
        }
        ReactBad();
    }

    private void ReactWell()
    {
        clientNode.RandomizeGoodReaction();
        conversant.SetDialogue(clientNode.goodReaction);
        conversant.HandleDialogue();
        Pay();
        startTimer = true;
    }

    private void ReactBad()
    {
        clientNode.RandomizeBadReaction();
        conversant.SetDialogue(clientNode.badReaction);
        conversant.HandleDialogue();
    }

    private void Pay()
    {
        EconomyManager.instance.AddMoney(payment);
    }

    private void ArriveAnimation()
    {
        arriveAnimation = true;
    }

    private void MoveClientHorizontal(Transform _transform)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x = Mathf.Lerp(transform.localPosition.x, _transform.localPosition.x, Time.deltaTime * ClientManager.instance.GetHorizontalVelocity());

        transform.localPosition = newPosition;
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

    public CocktailNode GetOrder()
    {
        return order;
    }
    public AIConversant GetConversant()
    {
        return conversant;
    }
    public bool GetIsLocated()
    {
        return isLocated;
    }
    public bool GetCanLeave()
    {
        return canLeave;
    }

    public bool GetHitted()
    {
        return hitted;
    }

    public void SetClientNode(ClientNode _clientNode)
    {
        clientNode = _clientNode;
    }
    public void SetCanLeave(bool state)
    {
        canLeave = state;
    }
}
using Dialogue;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] private int maxHitsToGo; 

    [Header("Timer")]
    [SerializeField] private float maxTime; 
    private float time;

    private bool isLocated;

    private ClientNode clientNode;

    private bool hitted;

    private bool triggerSetted;
    private bool wellReacted;
    private bool badReacted;

    private int currentsHits;

    private void Awake()
    {
        conversant = GetComponent<AIConversant>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        //boxCollider.enabled = false;
        arriveAnimation = false;
        leaveAnimation = false;
        startTimer = false;
        time = 0;

        canLeave = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        isLocated = false;
        hitted = false;
        wellReacted = false;

        currentsHits = 0;
    }

    private void Start()
    {
        transform.localPosition = ClientManager.instance.GetSpawnPosition().localPosition;
        ArriveAnimation();
    }

    private void Update()
    {
        Lerps();

        if (canLeave && clientNode.notNeedTakeDrink && !clientNode.hitToGo)
        {
            startTimer = true;
        }

        if (startTimer && !conversant.GetPlayerConversant().HasNext())
        {
            Timer();
        }

        if (!triggerSetted)
        {
            GetComponent<DialogueTrigger>().SetTriggerAction("ActiveCollision");
            GetComponent<DialogueTrigger>().SetOnTriggerEvent(EnableCollider);
            triggerSetted = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cocktail") && CursorManager.instance.IsMouseUp() && !clientNode.notNeedTakeDrink)
        {
            

            LiquidManager liquidManagerResult = collision.GetComponentInChildren<LiquidManager>();

            string findError = CalculateDrink.instance.CalculateResultDrink(
                    liquidManagerResult.GetParticleTypes(),
                    liquidManagerResult.GetDrinkState(),
                    collision.GetComponentInChildren<SpriteRenderer>().sprite,
                    collision.GetComponentInChildren<InsideDecorations>().GetDecorations(),
                    order.type);

            Debug.Log(findError);

            if (clientNode.acceptsAll && collision.transform.GetChild(2).GetComponent<LiquidManager>().GetCurrentLiquid() > 0)
            {
                bool state = false; ;
                if (findError == "Good" || findError == "BadGlass")
                {
                    state = true;
                }
                   
                Debug.Log(state);
               ReactWell(state);
                Destroy(collision.gameObject);
                return;
            }
            else if(clientNode.acceptsAll && collision.transform.GetChild(2).GetComponent<LiquidManager>().GetCurrentLiquid() == 0)
            {
                ReactBadAcctepAll();
                Destroy(collision.gameObject);
                return;
            }

            FindCoctelError(findError, collision);

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer") && !startTimer && clientNode.canBeHitted)
        {
            BloodParticleSystemHandle.Instance.SpawnBlood(collision.transform.position, Vector3.down);

            hitted = true;
            currentsHits++;
            AudioManager.instance.PlaySFX("ClientHit");

            clientNode.RandomizeHitReaction();

            if(clientNode.totalHits <= 1)
                conversant.SetDialogue(clientNode.hitReaction);

            conversant.HandleDialogue();


            if ((clientNode.hitToGo && currentsHits == clientNode.totalHits))
            {
                startTimer = true;
                return;
            }
            
        }
    }

    public void InitClient()
    {
        //boxCollider.enabled = true;

        if(clientNode.regularHitReactions)
        {
            clientNode.hitReactions = ClientManager.instance.GetRegularClientHitDialogues();
        }

        int randomOrder = Random.Range(0, clientNode.possibleOrders.Count);
        order = clientNode.possibleOrders[randomOrder];
        payment = order.price;

        Dialogue.Dialogue currentDialogue = clientNode.dialogues[randomOrder];
        conversant.SetDialogue(currentDialogue);

        spriteRenderer.sprite = clientNode.sprite;
        spriteRenderer.flipX = true;
    }

    private void FindCoctelError(string findError, Collider2D collision)
    {
        if (findError == "Good")
        { 
            if(clientNode.careIces)
            {
                int ices = collision.GetComponentInChildren<InsideDecorations>().GetDecorations().ElementAt(0).Value;
                if (ices != clientNode.cuantityOfIce)
                {
                    conversant.SetDialogue(clientNode.noIceReaction);
                    conversant.HandleDialogue();
                }
                else
                {
                    ReactWell(true);
                }
            }
            else
            {
                ReactWell(true);
            }
            
        }
        else if (findError == "BadGlass")
        {
            if (clientNode.dontCareGlass)
            {
                ReactWell(true);
            }
            else if (clientNode.badGlassReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.badGlassReaction);
                conversant.HandleDialogue();
            }
            
            else
                ReactBad();
        }
        else if (findError == "NoIce")
        {
            if (clientNode.dontCareGlass)
            {
                ReactWell(true);
            }
            else if (clientNode.noIceReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.noIceReaction);
                conversant.HandleDialogue();
            }      
            else
                ReactBad();
        }
        else if (findError == "MuchIce")
        {
            if (clientNode.muchIceReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.muchIceReaction);
                conversant.HandleDialogue();
            }
            else
                ReactBad();
        }
        else if (findError == "BadState")
        {
            if (clientNode.badStateReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.badStateReaction);
                conversant.HandleDialogue();
            }
            else
                ReactBad();
        }
        else if (findError == "BadIngredients")
        {
            if (clientNode.badIngredientsReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.badIngredientsReaction);
                conversant.HandleDialogue();
                if (clientNode.OnlyOneChance)
                {
                    badReacted = true;
                    startTimer = true;
                }
            }
            else
                ReactBad();
        }
    }

    private void ReactWell(bool isOk)
    {
        AudioManager.instance.PlaySFX("ClientHappy");
        clientNode.RandomizeGoodReaction();
        if(isOk)
            conversant.SetDialogue(clientNode.goodReaction);
        else
            conversant.SetDialogue(clientNode.badReaction);
        conversant.HandleDialogue();
        if(!clientNode.DontPay)
        {
            Pay();
        }
        if(!clientNode.HasMoraDialoguesPostOrder)
        {
            startTimer = true;
        }       
        wellReacted = true;
    }

    private void ReactBad()
    {
        AudioManager.instance.PlaySFX("ClientMad");
        clientNode.RandomizeBadReaction();
        conversant.SetDialogue(clientNode.badReaction);
        conversant.HandleDialogue();
        if (clientNode.OnlyOneChance)
        {
            badReacted = true;
            startTimer = true;
        }

    }

    private void ReactBadAcctepAll()
    {
        AudioManager.instance.PlaySFX("ClientMad");
        conversant.SetDialogue(clientNode.badIngredientsReaction);
        conversant.HandleDialogue();
    }

    private void Pay()
    {
        EconomyManager.instance.SetMoneyChanged(payment);
    }

    private void ArriveAnimation()
    {
        arriveAnimation = true;
    }

    private void Lerps()
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
            MoveClientHorizontal(ClientManager.instance.GetLeavePosition());
            if (transform.localPosition.x > ClientManager.instance.GetLeavePosition().localPosition.x - 0.01)
            {
                ClientManager.instance.CreateClient();
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

    public void Timer()
    {
        boxCollider.enabled = false;
        time += Time.deltaTime;
        if(time > maxTime)
        {
            startTimer = false;
            leaveAnimation = true;
        }
    }

    private void EnableCollider()
    {
        Debug.Log("Active Collision");
        boxCollider.enabled = true;
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

    public bool GetWellReacted()
    {
        return wellReacted;
    }

    public int GetCurrentsHit()
    {
        return currentsHits;
    }

    public bool GetBadReacted()
    {
        return badReacted;
    }

    public void SetClientNode(ClientNode _clientNode)
    {
        clientNode = _clientNode;
    }
    public void SetCanLeave(bool state)
    {
        canLeave = state;
    }

    public void SetTimer(bool state)
    {
        startTimer = state;
    }

    public void SetHitted(bool state)
    {
        hitted = state;
    }


}
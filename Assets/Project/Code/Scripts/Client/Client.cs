using Dialogue;
using System.Collections.Generic;
using System.Linq;
using UI;
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
    private bool leave;

    [Header("Client Dialogue")]
    private AIConversant conversant;
    [SerializeField] private int maxHitsToGo;

    [Header("Timer")]
    [SerializeField] private float maxTime;

    private bool isLocated;

    private ClientNode clientNode;

    private bool hitted;

    private bool triggerSetted;
    private bool wellReacted;
    private bool badReacted;
    private bool activeCollision;
    private bool isUp = true;
    private bool hasToMoveY = true;

    private int currentsHits;
    private PlayerConversant player;

    private DialogueUI dialogueUI;

    private void Awake()
    {
        conversant = GetComponent<AIConversant>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.enabled = false;
        arriveAnimation = false;
        leaveAnimation = false;

        canLeave = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        isLocated = false;
        hitted = false;
        wellReacted = false;
        activeCollision = false;

        currentsHits = 0;
    }

    private void Start()
    {
        transform.localPosition = ClientManager.instance.GetSpawnPosition().localPosition;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        ArriveAnimation();
    }

    private void Update()
    {
        Lerps();

        if (canLeave && clientNode.notNeedTakeDrink && !clientNode.hitToGo)
        {
            leaveAnimation = true;
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

            Dictionary<ItemNode, int> decorations;
            if (collision.GetComponentInChildren<InsideDecorations>().GetDecorations() == null)
                decorations = null;
            else
                decorations = collision.GetComponentInChildren<InsideDecorations>().GetDecorations();

            string findError = CalculateDrink.instance.CalculateResultDrink(
                    liquidManagerResult.GetParticleTypes(),
                    liquidManagerResult.GetDrinkState(),
                    collision.GetComponentInChildren<SpriteRenderer>().sprite,
                    decorations,
                    order.type);

            Debug.Log(findError);

            if (clientNode.acceptsAll && collision.transform.GetChild(2).GetComponent<LiquidManager>().GetCurrentLiquid() > 0)
            {
                bool state = false; ;
                if (findError == "Good" || findError == "BadGlass" || findError == "NoIce")
                {
                    state = true;
                }

                ReactWell(state);
                Destroy(collision.gameObject);
                return;
            }
            else if (clientNode.acceptsAll && collision.transform.GetChild(2).GetComponent<LiquidManager>().GetCurrentLiquid() == 0)
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
        if (collision.CompareTag("Hammer") && !leaveAnimation && clientNode.canBeHitted)
        {
            if (clientNode.payAfterHit)
            {
                Pay();
            }

            hitted = true;
            currentsHits++;
            AudioManager.instance.PlaySFX("ClientHit");

            clientNode.RandomizeHitReaction();

            if (clientNode.totalHits <= 1)
                conversant.SetDialogue(clientNode.hitReaction);

            conversant.HandleDialogue(clientNode.pitch);


            if ((clientNode.hitToGo && currentsHits == clientNode.totalHits))
            {
                leaveAnimation = true;
                return;
            }

        }
    }

    public void InitClient()
    {
        //boxCollider.enabled = true;

        if (clientNode.regularHitReactions)
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
        if (findError == "Good" )
        {
            if (clientNode.careIces)
            {
                int ices = collision.GetComponentInChildren<InsideDecorations>().GetDecorations().ElementAt(0).Value;
                if (ices != clientNode.cuantityOfIce )
                {
                    conversant.SetDialogue(clientNode.noIceReaction);
                    conversant.HandleDialogue(clientNode.pitch);
                }
                else
                {
                    ReactWell(true);
                }
            }
            else if (clientNode.wantDrug && !collision.GetComponentInChildren<InsideDecorations>().GetHasDrug())
            {
                ReactBad();
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
                conversant.HandleDialogue(clientNode.pitch);
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
                conversant.HandleDialogue(clientNode.pitch);
            }
            else
                ReactBad();
        }
        else if (findError == "MuchIce")
        {
            if (clientNode.muchIceReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.muchIceReaction);
                conversant.HandleDialogue(clientNode.pitch);
            }
            else
                ReactBad();
        }
        else if (findError == "BadState")
        {
            if (clientNode.badStateReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.badStateReaction);
                conversant.HandleDialogue(clientNode.pitch);
            }
            else
                ReactBad();
        }
        else if (findError == "BadIngredients")
        {
            if (clientNode.badIngredientsReaction != ClientManager.instance.GetEmptyDialogue())
            {
                conversant.SetDialogue(clientNode.badIngredientsReaction);
                conversant.HandleDialogue(clientNode.pitch);
                if (clientNode.onlyOneChance)
                {
                    badReacted = true;
                    leaveAnimation = true;
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
        if (isOk)
            conversant.SetDialogue(clientNode.goodReaction);
        else
            conversant.SetDialogue(clientNode.badReaction);

        conversant.HandleDialogue(clientNode.pitch);

        if (!clientNode.dontPay)
        {
            Pay();
        }
        if (!clientNode.hasMoraDialoguesPostOrder)
        {
            leaveAnimation = true;
        }
        wellReacted = true;
    }

    private void ReactBad()
    {
        AudioManager.instance.PlaySFX("ClientMad");
        clientNode.RandomizeBadReaction();
        conversant.SetDialogue(clientNode.badReaction);

        conversant.HandleDialogue(clientNode.pitch);
        if (clientNode.onlyOneChance)
        {
            badReacted = true;
            leaveAnimation = true;
        }

    }

    private void ReactBadAcctepAll()
    {
        AudioManager.instance.PlaySFX("ClientMad");
        conversant.SetDialogue(clientNode.badIngredientsReaction);
        conversant.HandleDialogue(clientNode.pitch);
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
        if(hasToMoveY)
        {
            MoveClientVertical();
        }

        if (arriveAnimation)
        {
            MoveClientHorizontal(ClientManager.instance.GetClientPosition());
            if (transform.localPosition.x > ClientManager.instance.GetClientPosition().localPosition.x - 0.01 )
            {
                arriveAnimation = false;
                isLocated = true;
                conversant.HandleDialogue(clientNode.pitch);
            }
        }
        else if (leaveAnimation)
        {
            if (TypeWriterEffect.isTextCompleted && Input.GetMouseButtonDown(0) && !player.HasNext())
            {
                dialogueUI.DestroyAllBubbles();
                leave = true;
            }
        }

        if (leave)
        {
            MoveClientHorizontal(ClientManager.instance.GetLeavePosition());
            if (transform.localPosition.x > ClientManager.instance.GetLeavePosition().localPosition.x - 0.01)
            {
                ClientManager.instance.CreateClient();
                Destroy(gameObject);
            }
        }

        if (activeCollision && !boxCollider.enabled && !leaveAnimation)
        {
            boxCollider.enabled = true;
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
        Vector3 newPosition = transform.localPosition;
        if(isUp)
        {
            newPosition.y = Mathf.Lerp(transform.localPosition.y, ClientManager.instance.GetPositveJumpPosition().localPosition.y, Time.deltaTime * ClientManager.instance.GetVerticalVelocity());
            if(newPosition.y >= ClientManager.instance.GetPositveJumpPosition().localPosition.y - 0.01)
            {
                isUp = false;
            }
        }
        else
        {
            newPosition.y = Mathf.Lerp(transform.localPosition.y, ClientManager.instance.GetNegativeJumpPosition().localPosition.y, Time.deltaTime * ClientManager.instance.GetVerticalVelocity());
            if (newPosition.y <= ClientManager.instance.GetNegativeJumpPosition().localPosition.y + 0.01)
            {
                isUp = true;
            }
        }
        transform.localPosition = newPosition;
    }

    private void EnableCollider()
    {
        Debug.Log("Active Collision");
        activeCollision = true;
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
    public void SetLeave(bool leave)
    {
        this.leave = leave;
    }
    public bool GetLeave()
    {
        return leave;
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
    public bool GetArrive()
    {
        return arriveAnimation;
    }

    public bool GetLeaveAnimation()
    {
        return leaveAnimation;
    }
    public void SetClientNode(ClientNode _clientNode)
    {
        clientNode = _clientNode;
    }
    public void SetCanLeave(bool state)
    {
        canLeave = state;
    }

    public void SetHitted(bool state)
    {
        hitted = state;
    }

    public void SetLeaveAnimation(bool state)
    {
        leaveAnimation = state;
    }
    public void SetDialogueUI(DialogueUI dialogueUI)
    {
        this.dialogueUI = dialogueUI;
    }

    public void SetActiveCollision(bool state)
    {
        activeCollision = state;
    }

    public void SetHasToMoveY(bool state)
    {
        hasToMoveY = state;
    }

}
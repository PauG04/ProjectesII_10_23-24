using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] Transform clientParent;
    [SerializeField] GameObject client;
    private GameObject currentClient;
    private Client currentClientScript;
    private bool nextClientIsImportant;
    private ImportantClientNode nextImportantClient;

    [SerializeField] private List<Dialogue.Dialogue> regularClientDialogues;
    [SerializeField] private Dialogue.Dialogue badReactionDialogue;
    [SerializeField] private Dialogue.Dialogue goodReactionDialogue;

    [Header("Client Position")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform clientPosition;
    [SerializeField] private Transform DestroyPosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float verticalVelocity;

    [SerializeField] private List<Sprite> clientSprites;
    [SerializeField] private TutorialManager tutorial;

    [SerializeField] private bool isNormalDay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        nextClientIsImportant = false;
        if(isNormalDay)
        {
            CreateNewClient();
        }
    }

    public void CreateNewClient()
    {
        if(nextClientIsImportant)
            CreateImportantClient(nextImportantClient);
        else
            CreateRegularClient();
        nextClientIsImportant = false;
    }

    private void CreateRegularClient()
    {
        currentClient = Instantiate(client, clientParent);
        currentClientScript = currentClient.GetComponent<Client>();
    }
    private void CreateImportantClient(ImportantClientNode node)
    {
        currentClient = Instantiate(client, clientParent);
        currentClientScript = currentClient.GetComponent<Client>();
        currentClientScript.SetNotNeedTakeDrink(node.notNeedTakeDrink);
        currentClientScript.GetConversant().SetDialogue(node.currentDialogue);
        currentClientScript.SetSprite(node.sprite);
        if((node.currentDialogue.name == "HouseKeeper" || node.currentDialogue.name == "TutorialDialogue1") && tutorial != null)
        {
            tutorial.SetIsFriend(true);
            currentClientScript.SetIsTutorial(true);
        }
    }

    #region GETTERS
    public List<Sprite> GetClientSprites()
    {
        return clientSprites;
    }
    public Client GetCurrentClientScript()
    {
        return currentClientScript;
    }
    public List<Dialogue.Dialogue> GetRegularClientDialogues()
    {
        return regularClientDialogues;
    }
    public Dialogue.Dialogue GetBadReactionDialogue()
    {
        return badReactionDialogue;
    }
    public Dialogue.Dialogue GetGoodReactionDialogue()
    {
        return goodReactionDialogue;
    }
    public Transform GetSpawnPosition()
    {
        return spawnPosition;
    }
    public Transform GetClientPosition()
    {
        return clientPosition;
    }
    public Transform GetLeavePosition()
    {
        return DestroyPosition;
    }
    public float GetMaxYPosition()
    {
        return maxYPosition;
    }
    public float GetHorizontalVelocity()
    {
        return horizontalVelocity;
    }
    public float GetVerticalVelocity()
    {
        return verticalVelocity;
    }
    #endregion

    #region
    public void SetNextClientIsImportant(bool value)
    {
        nextClientIsImportant = value;
    }
    public void SetNextImportantClient(ImportantClientNode node)
    {
        nextImportantClient = node;
    }
    #endregion
}
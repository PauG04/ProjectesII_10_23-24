using Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] private List<ClientNode> currentDayClients;
    private int clientCounter;

    [SerializeField] Transform clientParent;
    [SerializeField] GameObject client;
    private GameObject currentClient;
    private Client currentClientScript;
    private ClientNode currentClientNode;


    [Header("Dialogue")]
    [SerializeField] private List<Dialogue.Dialogue> regularClientDialogues;
    [SerializeField] private List<Dialogue.Dialogue> regularGoodReactionDialogues;
    [SerializeField] private List<Dialogue.Dialogue> regularBadReactionDialogues;
    [SerializeField] private List<Dialogue.Dialogue> regularClientHitDialogues;
    [SerializeField] private List<Sprite> clientSprites;

    [Header("Client Position")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform clientPosition;
    [SerializeField] private Transform DestroyPosition;
    [SerializeField] private float horizontalVelocity;

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

        clientCounter = 0;

        CreateClient();
    }

    public void CreateClient()
    {
        currentClientNode = currentDayClients[clientCounter];
        clientCounter++;

        if (currentClientNode.clientName == "Regular")
        {
            currentClientNode.dialogues = regularClientDialogues;
            currentClientNode.goodReactions = regularGoodReactionDialogues;
            currentClientNode.badReactions = regularBadReactionDialogues;
        }

        currentClient = Instantiate(client, clientParent);
        currentClientScript = currentClient.GetComponent<Client>();
        currentClientScript.SetClientNode(currentClientNode);
        currentClientScript.InitClient();
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
    public List<Dialogue.Dialogue> GetRegularGoodReactionDialogues()
    {
        return regularGoodReactionDialogues;
    }
    public List<Dialogue.Dialogue> GetRegularBadReactionDialogues()
    {
        return regularBadReactionDialogues;
    }
    public List<Dialogue.Dialogue> GetRegularClientHitDialogues()
    {
        return regularClientHitDialogues;
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
    public float GetHorizontalVelocity()
    {
        return horizontalVelocity;
    }
    #endregion
}
using Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] private List<ClientNode> currentDayClients;
    private int clientCounter;

    [SerializeField] private Transform clientParent;
    [SerializeField] private GameObject client;
    private GameObject currentClient;
    private Client currentClientScript;
    private ClientNode currentClientNode;

    [Header("Dialogue")]
    [SerializeField] private List<Dialogue.Dialogue> regularClientHitDialogues;
    [SerializeField] private List<Sprite> clientSprites;
    [SerializeField] private Dialogue.Dialogue emptyDialogue;

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
        if (clientCounter >= currentDayClients.Count)
        {
            TimeManager.instance.StopTime();
        }
        else
        {
            currentClientNode = currentDayClients[clientCounter];
            clientCounter++;

            currentClient = Instantiate(client, clientParent);
            currentClientScript = currentClient.GetComponent<Client>();
            currentClientScript.SetClientNode(currentClientNode);
            currentClientScript.InitClient();
        }
    }

    #region GETTERS
    public Dialogue.Dialogue GetEmptyDialogue()
    {
        return emptyDialogue;
    }
    public List<Sprite> GetClientSprites()
    {
        return clientSprites;
    }
    public Client GetCurrentClientScript()
    {
        return currentClientScript;
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

    public ClientNode GetClient()
    {
        return currentClientNode;
    }

    public GameObject GetClientObject()
    {
        return currentClientScript.gameObject;
    }
    #endregion
}

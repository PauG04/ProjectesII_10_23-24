using Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] private DayManager dayManager;
    [SerializeField] private DaysEventsController daysEventsController;
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

        daysEventsController.ActiveEventDay(dayManager.GetCurrentDay());
        currentDayClients = dayManager.GetClients(dayManager.GetCurrentDay());

        CreateClient();
    }

    public void CreateClient()
    {
        if (dayManager.GetCurrentDay() <= dayManager.GetLastDay())
        {
            if (clientCounter >= currentDayClients.Count)
            {
                dayManager.SetCurrentDay(1);
                daysEventsController.ActiveEventDay(dayManager.GetCurrentDay());
                currentDayClients = dayManager.GetClients(dayManager.GetCurrentDay());
                clientCounter = 0;

                Debug.Log("Fin del día");
                CreateClient();

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
        else
        {
            daysEventsController.DesactiveLastDay();
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

    public void SetNewCLient(ClientNode client, int position)
    {
        currentDayClients.Insert(position, client);
    }

    public void PassClient()
    {
        Destroy(currentClient);
        CreateClient();
    }
}

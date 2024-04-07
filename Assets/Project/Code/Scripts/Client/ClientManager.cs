using Dialogue;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
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

    [Header("Day Transition")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private DialogueUI dialogueCanvas;
    [SerializeField] private GameObject endOfDay;

    private bool isCourtainClosed;
    private bool dayEnded;

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
    public void EndDay()
    {
        levelLoader.CloseAnimation();
        dayEnded = true;
        Invoke("ShowEndOfDay", 1f);
    }

    private void ShowEndOfDay()
    {
        endOfDay.SetActive(true);
        isCourtainClosed = true;
    }
    public void LoadDay()
    {
        levelLoader.OpenAnimation();
        endOfDay.SetActive(false);
        dayEnded = false;
        dialogueCanvas.DestroyAllBubbles();

        dayManager.SetCurrentDay(1);
        daysEventsController.ActiveEventDay(dayManager.GetCurrentDay());
        currentDayClients = dayManager.GetClients(dayManager.GetCurrentDay());
        clientCounter = 0;

        EconomyManager.instance.ResetDailyEarnings();

        CreateClient();

        isCourtainClosed = false;
    }
    public void CreateClient()
    {
        if (dayManager.GetCurrentDay() <= dayManager.GetLastDay())
        {
            if (clientCounter >= currentDayClients.Count)
            {
                EndDay();
            }
            else
            {
                currentClientNode = currentDayClients[clientCounter];
                clientCounter++;

                currentClient = Instantiate(client, clientParent);
                currentClientScript = currentClient.GetComponent<Client>();
                currentClientScript.SetDialogueUI(dialogueCanvas);
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
        if(!dayEnded)
        {
            return currentClientNode;
        }

        return null;      
    }

    public GameObject GetClientObject()
    {
        if(!dayEnded)
        {
            return currentClientScript.gameObject;
        }

        return null;
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

    public bool GetCourtainClosed()
    {
        return isCourtainClosed;
    }
}

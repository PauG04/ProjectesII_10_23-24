using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] Transform clientParent;
    [SerializeField] GameObject client;
    private GameObject currentClient;

    [SerializeField] private List<Dialogue.Dialogue> regularClientDialogues;

    [Header("Client Position")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform clientPosition;
    [SerializeField] private Transform DestroyPosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float verticalVelocity;

    [SerializeField] private List<Sprite> clientSprites;

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
        CreateNewClient();
    }

    public List<Sprite> GetClientSprites()
    {
        return clientSprites;
    }

    public void CreateNewClient()
    {
        currentClient = Instantiate(client, clientParent);
    }

    public void CreateNewImportantClient(ImportantClientNode node)
    {
        currentClient = Instantiate(client, clientParent);
    }

    #region GETTERS
    public List<Dialogue.Dialogue> GetRegularClientDialogues()
    {
        return regularClientDialogues;
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
}
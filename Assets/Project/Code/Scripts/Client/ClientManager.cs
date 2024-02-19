using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] Transform clientParent;
    [SerializeField] GameObject client;
    private GameObject currentClient;

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
    }

    public List<Sprite> GetClientSprites()
    {
        return clientSprites;
    }

    public void CreateNewClient()
    {
        Client temp = currentClient.GetComponent<Client>();
        currentClient = Instantiate(client, clientParent);
        currentClient.GetComponent<Client>().InitClient(temp);
        Destroy(temp);
    }
}
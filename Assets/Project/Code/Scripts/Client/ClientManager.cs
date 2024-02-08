using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance { get; private set; }

    [SerializeField] GameObject client;
    [SerializeField] Transform clientParent;

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
        Instantiate(client, clientParent);
    }
}
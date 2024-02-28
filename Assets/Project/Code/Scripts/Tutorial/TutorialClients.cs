using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClients : MonoBehaviour
{
    [SerializeField] private ImportantClientNode houseKeeper;
    [SerializeField] private ImportantClientNode friend;

    private void Start()
    {
        InitHouseKeeper();
    }

    private void InitHouseKeeper()
    {
        //ClientManager.instance.SetNextImportantClient(houseKeeper);
        //ClientManager.instance.SetNextClientIsImportant(true);
        //ClientManager.instance.CreateNewClient();
        ClientManager.instance.SetNextClientIsImportant(true);
        ClientManager.instance.SetNextImportantClient(friend);
        ClientManager.instance.CreateNewClient();
    }
}

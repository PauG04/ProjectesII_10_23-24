using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Client", menuName = "Client", order = 5)]
public class ImportantClient : ScriptableObject
{
    public Client client;
    public string clientName;
    public Sprite sprite;
    public DialogueNode dialogue;
}

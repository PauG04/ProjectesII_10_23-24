using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Client", menuName = "Client", order = 5)]
public class ImportantClientNode : ScriptableObject
{
    public string clientName;
    public bool notNeedTakeDrink;
    public Sprite sprite;
    public List<Dialogue.Dialogue> dialogues;
    public Dialogue.Dialogue currentDialogue;
    private int counter;

    private void Awake()
    {
        counter = 0;
        currentDialogue = dialogues[counter];
    }
    public void NextDialogue()
    {
        counter++;
        currentDialogue = dialogues[counter];
    }
}

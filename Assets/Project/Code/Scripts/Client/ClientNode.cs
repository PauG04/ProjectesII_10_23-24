using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Client", menuName = "Client", order = 5)]
public class ClientNode : ScriptableObject
{
    public string clientName;
    public Sprite sprite;

    public bool notNeedTakeDrink;

    public bool acceptsAll;
    public bool acceptsNothing;
    public List<CocktailNode> possibleOrders;

    public List<Dialogue.Dialogue> dialogues;
    public List<Dialogue.Dialogue> goodReactions;
    public List<Dialogue.Dialogue> badReactions;
    public Dialogue.Dialogue dialogue;
    public Dialogue.Dialogue goodReaction;
    public Dialogue.Dialogue badReaction;

    private void OnEnable()
    {
        dialogue = dialogues[0];
        goodReaction = goodReactions[0];
        badReaction = badReactions[0];
    }

    public void RandomizeDialogue()
    {
        dialogue = dialogues[Random.Range(0, dialogues.Count)];
    }
    public void RandomizeGoodReaction()
    {
        goodReaction = goodReactions[Random.Range(0, goodReactions.Count)];
    }
    public void RandomizeBadReaction()
    {
        badReaction = badReactions[Random.Range(0, badReactions.Count)];
    }
}

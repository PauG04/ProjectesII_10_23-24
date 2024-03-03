using Dialogue;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Client", menuName = "Client", order = 5)]
public class ClientNode : ScriptableObject
{
    public string clientName;
    public Sprite sprite;

    public bool hitToGo;
    public bool canBeHitted;
    public bool regularHitReactions;

    public bool notNeedTakeDrink;
    public bool acceptsAll;
    public List<CocktailNode> possibleOrders;

    public List<Dialogue.Dialogue> dialogues;
    public List<Dialogue.Dialogue> goodReactions;
    public List<Dialogue.Dialogue> badReactions;
    public List<Dialogue.Dialogue> hitReactions;
    public Dialogue.Dialogue dialogue;
    public Dialogue.Dialogue goodReaction;
    public Dialogue.Dialogue badReaction;
    public Dialogue.Dialogue hitReaction;
    public Dialogue.Dialogue badGlassReaction;
    public Dialogue.Dialogue noIceReaction;
    public Dialogue.Dialogue muchIceReaction;
    public Dialogue.Dialogue badStateReaction;
    public Dialogue.Dialogue badIngredientsReaction;

    private void OnEnable()
    {
        dialogue = dialogues[0];
        goodReaction = goodReactions[0];
        badReaction = badReactions[0];
        hitReaction = hitReactions[0];
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
    public void RandomizeHitReaction()
    {
        hitReaction = hitReactions[Random.Range(0, hitReactions.Count)];
    }
}

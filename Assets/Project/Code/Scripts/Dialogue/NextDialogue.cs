using UnityEngine;
using Dialogue;
using UI;

public class NextDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueCanvas;

    private PlayerConversant playerConversant;

    private void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
    }

    private void OnMouseDown()
    {

        if (TypeWriterEffect.isTextCompleted && playerConversant.IsActive())
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            //else if(ClientManager.instance.GetCurrentClientScript().GetCanLeave())
            //{
            //    ClientManager.instance.GetCurrentClientScript().SetLeave(true);
            //    dialogueCanvas.DestroyAllBubbles();
            //}
        }
    }
}

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
        Debug.Log(ClientManager.instance.GetCurrentClientScript().GetLeaveAnimation());

        if (TypeWriterEffect.isTextCompleted)
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

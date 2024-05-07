using Dialogue;
using UnityEngine;

public class OldEvent : MonoBehaviour
{
    [SerializeField] private Dialogue.Dialogue dialogue;
    private Vector3 initPosition;

    private void Awake()
    {
        initPosition = transform.localPosition;
        initPosition.x -= 1;
    }
    private void Update()
    {
        if(transform.localPosition.y < -5f)
        {
            transform.localPosition = initPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Client") && gameObject.GetComponent<DragItems>().GetWasOnTheTable() && TypeWriterEffect.isTextCompleted)
        {
            collision.GetComponent<AIConversant>().SetDialogue(dialogue);
            collision.GetComponent<AIConversant>().HandleDialogue(1f);
            Destroy(gameObject);
            collision.GetComponent<Client>().SetLeaveAnimation(true);
        }
    }
}

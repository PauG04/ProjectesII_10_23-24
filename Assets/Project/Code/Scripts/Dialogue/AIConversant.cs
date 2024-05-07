using UnityEngine;

namespace Dialogue
{
	public class AIConversant : MonoBehaviour
	{
		[SerializeField] private Dialogue dialogue = null;
		[SerializeField] private string conversantName;
		[SerializeField] private PlayerConversant playerConversant;

		private Client client;

		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            client = GetComponent<Client>();
        }

        public void HandleDialogue(float pitch)
		{
            if (dialogue == null)
            {
                return;
            }

            playerConversant.StartDialogue(this, dialogue);
        }

        private void Update()
        {
            if (playerConversant.IsActive() && !client.GetCanLeave())
            {
                if (!playerConversant.HasNext())
                {
                    client.SetCanLeave(true);
                }
            }
                     
        }
        public string GetName()
		{
			return conversantName;
		}
        public void SetDialogue(Dialogue dialogue)
        {
            this.dialogue = dialogue;
        }
    }
}
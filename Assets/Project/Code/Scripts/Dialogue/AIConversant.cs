using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	public class AIConversant : MonoBehaviour
	{
		[SerializeField] private Dialogue dialogue = null;
		[SerializeField] private string conversantName;
		[SerializeField] private PlayerConversant playerConversant;

		private Client client;


		private int i;
		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            client = GetComponent<Client>();
        }

        public void HandleDialogue()
		{
			if(dialogue == null)
			{
				return;
			}

			playerConversant.StartDialogue(this, dialogue);
		}

        private void Update()
        {
            if (playerConversant.IsActive() && !client.GetCanLeave())
			{
                if (!playerConversant.HasNext() && playerConversant.GetIsTextDone())
                {
                    client.SetCanLeave(true);
                }
            }           
        }
		public PlayerConversant GetPlayerConversant()
		{
			return playerConversant;
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
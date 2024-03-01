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
			if(playerConversant.IsActive())
			{
                if (!playerConversant.HasNext())
                {
                    client.SetCanLeave(true);
                }
            }           
        }

		public void SetDialogue(Dialogue dialogue)
		{
			this.dialogue = dialogue;
		}
        public string GetName()
		{
			return conversantName;
		}
		
	}
}
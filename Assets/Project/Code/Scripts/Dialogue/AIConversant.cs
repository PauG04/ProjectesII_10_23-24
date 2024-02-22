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

		private bool hasExecuted;
		
		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			hasExecuted = false;
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
        private void OnMouseDown()
		{
			Debug.Log("PlayerPressed");
			if (!playerConversant.GetCanContinue() && !hasExecuted)
			{
				playerConversant.SetCanContinue(true);
				if (playerConversant.HasNext())
				{
                    playerConversant.Next();
                }
                hasExecuted = true;
			}
		}
        private void OnMouseUp()
        {
             hasExecuted = false;
        }
        public string GetName()
		{
			return conversantName;
		}
		
	}
}
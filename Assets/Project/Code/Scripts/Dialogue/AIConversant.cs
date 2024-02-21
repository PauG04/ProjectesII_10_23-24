using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
	public class AIConversant : MonoBehaviour
	{
		[SerializeField] private Dialogue dialogue = null;
		[SerializeField] private string conversantName;
		[SerializeField] private PlayerConversant playerConversant;

		private bool hasExecuted;
		
		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			hasExecuted = false;

        }	
	
		public void HandleDialogue()
		{
			if(dialogue == null)
			{
				return;
			}
			
			playerConversant.StartDialogue(this, dialogue);
		}
		private void OnMouseDown()
		{
			Debug.Log("PlayerPressed");
			if (!playerConversant.GetCanContinue() && !hasExecuted)
			{
				playerConversant.SetCanContinue(true);
				playerConversant.Next();
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
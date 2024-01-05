using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

namespace Dialogue
{
	public class AIConversant : MonoBehaviour
	{
		[SerializeField] private Dialogue dialogue = null;
		[SerializeField] private string conversantName;
		[SerializeField] private PlayerConversant playerConversant;
		
		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
		}	
	
 		public CursorType GetCursorType()
		{
			return CursorType.Dialogue;
		}
		public void HandleDialogue(PlayerConversant playerConversant)
		{
			if(dialogue == null)
			{
				return;
			}

			playerConversant.StartDialogue(this, dialogue);
		}
		private void OnMouseDown()
		{
			HandleDialogue(playerConversant);
		}
		public string GetName()
		{
			return conversantName;
		}
		
	}
}
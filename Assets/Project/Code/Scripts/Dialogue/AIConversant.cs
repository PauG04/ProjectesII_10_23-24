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
		
		[HideInInspector] public bool stopDialogue;
		
		protected void Awake()
		{
			playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			stopDialogue = false;
		}	
	
		public void HandleDialogue()
		{
			if(dialogue == null)
			{
				return;
			}
			
			stopDialogue = false;
			playerConversant.StartDialogue(this, dialogue);
		}
		private void OnMouseDown()
		{
			//HandleDialogue();
		}
		
		public void StopDialogue()
		{
			stopDialogue = true;
		}
		
		public void ContinueDialogue()
		{
			stopDialogue = false;
		}
		
		public string GetName()
		{
			return conversantName;
		}
		
	}
}
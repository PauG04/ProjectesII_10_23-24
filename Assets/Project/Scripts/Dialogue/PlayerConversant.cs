using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
	
namespace Dialogue
{

	public class PlayerConversant : MonoBehaviour
	{
		//[SerializeField] private Dialogue testDialogue;
		[SerializeField] private string playerName;
		
		
		private Dialogue currentDialogue;
		private DialogueNode currentNode = null;
		private AIConversant currentConversant = null;
		private bool isChoosing = false;
		
		public event Action onConversationUpdated;

		/*
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(2f);
			StartDialogue(testDialogue);
		}
		*/
		public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
		{
			currentConversant = newConversant;
			currentDialogue = newDialogue;
			currentNode = currentDialogue.GetRootNode();
			TriggerEnterAction();
			onConversationUpdated();
		}
		
		public void Quit()
		{
			
			currentDialogue = null;
			TriggerExitAction();
			currentNode = null;
			isChoosing = false;
			currentConversant = null;
			onConversationUpdated();
		}
		
		public bool IsActive()
		{
			return currentDialogue != null;
		}
		
		public bool IsChoosing()
		{
			return isChoosing;
		}
		
		public string GetText()
		{
			if (currentNode == null)
			{
				return "";
			}
			
			return currentNode.GetText();
		}
		public string GetCurrentConversantName()
		{
			if (isChoosing)
			{
				return playerName;
			}
			return currentConversant.GetName();
		}
		public IEnumerable<DialogueNode> GetChoices()
		{
			return currentDialogue.GetPlayerChildren(currentNode);
		}
		public void SelectChoice(DialogueNode choseNode)
		{
			currentNode = choseNode;
			TriggerEnterAction();
			isChoosing = false;
			Next();
		}
		public void Next()
		{
			int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
			
			if(numPlayerResponses > 0)
			{
				isChoosing = true;
				TriggerExitAction();
				onConversationUpdated();
				return;
			}
			
			DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
			int randomIndex = UnityEngine.Random.Range(0, children.Count());
			TriggerExitAction();
			currentNode = children[randomIndex];
			TriggerEnterAction();
			onConversationUpdated();
		}
		
		public bool HasNext()
		{
			return currentDialogue.GetAllChildren(currentNode).Count() > 0;
		}
		
		private void TriggerEnterAction()
		{
			if(currentNode != null)
			{
				TriggerAction(currentNode.GetOnEnterAction());
			}
		}
		
		private void TriggerExitAction()
		{
			if(currentNode != null)
			{
				TriggerAction(currentNode.GetOnEnterAction());
			}
		}
		
		private void TriggerAction(string action)
		{
			if(action == "") 
			{
				return;
			}
			foreach (DialogueTrigger triggers in currentConversant.GetComponents<DialogueTrigger>())
			{
				triggers.Trigger(action);
			}
		}
	}

}
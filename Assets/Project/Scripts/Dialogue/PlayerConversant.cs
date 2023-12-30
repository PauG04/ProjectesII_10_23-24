using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
	public class PlayerConversant : MonoBehaviour
	{
		[SerializeField] private Dialogue currentDialogue;
		private DialogueNode currentNode = null;
		private bool isChoosing = false;
		
		protected void Awake()
		{
			currentNode = currentDialogue.GetRootNode();
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
		public IEnumerable<DialogueNode> GetChoices()
		{
			return currentDialogue.GetPlayerChildren(currentNode);
		}
		
		public void SelectChoice(DialogueNode choseNode)
		{
			currentNode = choseNode;
			isChoosing = false;
			Next();
		}
		
		public void Next()
		{
			int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
			
			if(numPlayerResponses > 0)
			{
				isChoosing = true;
				return;
			}
			
			DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
			// Temporal until it works
			int randomIndex = Random.Range(0, children.Count());
			
			currentNode = children[randomIndex];
		}
		
		public bool HasNext()
		{
			return currentDialogue.GetAllChildren(currentNode).Count() > 0;
		}
		
	}

}
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
		
		protected void Awake()
		{
			currentNode = currentDialogue.GetRootNode();
		}
		
		public string GetText()
		{
			if (currentNode == null)
			{
				return "";
			}
			
			return currentNode.GetText();
		}
		
		public void Next()
		{
			DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
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
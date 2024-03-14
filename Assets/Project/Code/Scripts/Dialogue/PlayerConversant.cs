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
		[SerializeField] private float secondsDialogueDelay = 1f;
		
		[Header("Dialogue Options")]
		private Dialogue currentDialogue;
		private DialogueNode currentNode = null;
		private AIConversant currentConversant = null;
		private bool isChoosing = false;
		private bool isStartingNewConversant = false;
		private bool isTextDone = false;

		/// TODO:
		/// 	find a better way to give the child to other objects instead of a numeric one
		private int currentChildNumber = 0;
		
		public event Action onConversationUpdated;

		private bool isTextRunning;
		public IEnumerator WriteTextWithDelay()
		{
			if (!currentNode.IsTextPaused())
			{
                isTextRunning = true;
                yield return new WaitForSeconds(secondsDialogueDelay);
                Next();
                isTextRunning = false;
            }
        }
        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
		{
            AudioManager.instance.PlaySFX("ClientTalk");
			isChoosing = false;

            currentConversant = newConversant;
			currentDialogue = newDialogue;
			currentNode = currentDialogue.GetRootNode();

			TriggerEnterAction();
			isStartingNewConversant = true;

            if (onConversationUpdated != null)
			{
				onConversationUpdated();
			}
			
			isStartingNewConversant = false;
			StopAllCoroutines();
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
        }
        public void Next()
		{
			AudioManager.instance.PlaySFX("ClientTalk");
			int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if (numPlayerResponses > 0)
            {
				isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            currentChildNumber = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[currentChildNumber];
            TriggerEnterAction();
            onConversationUpdated();
		}
		public bool HasNext()
		{
			return currentDialogue.GetAllChildren(currentNode).Count() > 0;
		}
		public bool IsNewConversant()
		{
			return isStartingNewConversant;
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
				TriggerAction(currentNode.GetOnExitAction());
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
        public int GetChildNumber()
		{
			return currentChildNumber;
		}
		public bool GetTextIsRunning()
		{
			return isTextRunning;
        }
		public bool GetCanContinue()
		{
			if (currentNode != null)
			{
                return currentNode.IsTextPaused();
            }
			return true;
		}
		public void SetIsTextDone(bool isTextDone)
		{
			this.isTextDone = isTextDone;
		}
		public bool GetIsTextDone()
		{
			return isTextDone;
		}
    }
}
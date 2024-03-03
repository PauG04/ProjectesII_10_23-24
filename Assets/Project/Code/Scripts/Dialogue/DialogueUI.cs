using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using TMPro;

namespace UI
{
	public class DialogueUI : MonoBehaviour
	{
		[Header("General Configuration")]
		private PlayerConversant playerConversant;
		[SerializeField] private Transform bubbleRoot;
		[SerializeField] private int maxNumberOfChilds = 7;

		[Header("Bubbles")]
		[SerializeField] private GameObject prefabAibubble;
        [SerializeField] private GameObject prefabPlayerbubble;
        [SerializeField] private GameObject separator;

		[Space(10)]	
		[SerializeField] private float timerDelay = 2.0f;

		private Coroutine coroutineRunning;

		private bool isSeparatorRunning;

        private void Start()
		{
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			playerConversant.onConversationUpdated += UpdateChat;

            DestroyChildrens(bubbleRoot);
        }

        private void Update()
		{
			if (playerConversant.IsActive())
			{
				if (!playerConversant.GetTextIsRunning())
				{
                    playerConversant.SetIsTextDone(false);
                    TypeWriterEffect.CompleteTextRevealed -= WriteText;
                }
                if (playerConversant.HasNext() && !playerConversant.IsChoosing() && !playerConversant.GetTextIsRunning())
				{
					if (coroutineRunning != null)
					{
                        StopCoroutine(SeparatorDelay());
						isSeparatorRunning = false;
                    }
					TypeWriterEffect.CompleteTextRevealed += WriteText;
                }
                else if ((playerConversant.IsChoosing() || !playerConversant.HasNext()) && !isSeparatorRunning)
				{
                    playerConversant.SetIsTextDone(true);
                    //coroutineRunning = StartCoroutine(SeparatorDelay());
                }
            }

			if (bubbleRoot.childCount > maxNumberOfChilds && bubbleRoot.childCount > 0)
			{
				Destroy(bubbleRoot.GetChild(0).gameObject);
			}
        }
		private void WriteText()
		{
            playerConversant.SetIsTextDone(true);
            coroutineRunning = StartCoroutine(playerConversant.WriteTextWithDelay());
        }
        private void UpdateChat()
		{
            if (playerConversant.IsNewConversant())
            {
                DestroyChildrens(bubbleRoot);
            }
            if (!playerConversant.IsActive())
			{
				return;
			}
			if(playerConversant.IsNewConversant())
			{
				StopAllCoroutines();
			}

            if (playerConversant.IsChoosing())
			{
                PlayerChoosing();
            }
            else 
			{
                AIBubble(playerConversant.GetText());
            }
        }
		private void PlayerChoosing()
		{
            foreach (DialogueNode choice in playerConversant.GetChoices())
			{
				/*
				if (playerConversant.GetCanContinue())
				{
					StopAllCoroutines();
				*/
                    PlayerBubble(choice.GetText());
                /*  
                    playerConversant.SelectChoice(choice);
                }
				*/
            }
        }
		private void AIBubble(string text)
		{
            GameObject AIBubble = Instantiate(prefabAibubble, bubbleRoot);
			AIBubble.GetComponentInChildren<TypeWriterEffect>().SetText(text);
        }
        private void PlayerBubble(string text)
		{
			GameObject PlayerBubble = Instantiate(separator, bubbleRoot);
        }
        private void DestroyChildrens(Transform root)
		{
			foreach (Transform item in root)
			{
				Destroy(item.gameObject);
			}
		}
        private IEnumerator SeparatorDelay()
        {
            isSeparatorRunning = true;
            yield return new WaitForSeconds(timerDelay);
            Instantiate(separator, bubbleRoot);
            isSeparatorRunning = false;
        }
        protected void OnDestroy()
		{
			playerConversant.onConversationUpdated -= UpdateChat;
		}
	}
}

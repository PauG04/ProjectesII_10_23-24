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

        private TextMeshProUGUI playerText;
        private TextMeshProUGUI AIText;

		private TypeWriterEffect AiWriterEffect;
		private TypeWriterEffect playerWriterEffect;

		private Coroutine coroutineRunning;

		private bool isSeparatorRunning;

        private void Start()
		{
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			playerConversant.onConversationUpdated += UpdateChat;
		    
		    AIText = prefabAibubble.GetComponentInChildren<TextMeshProUGUI>();
		    playerText = prefabPlayerbubble.GetComponentInChildren<TextMeshProUGUI>();

			AiWriterEffect = AIText.GetComponent<TypeWriterEffect>();
			playerWriterEffect = playerText.GetComponent<TypeWriterEffect>();

            DestroyChildrens(bubbleRoot);
        }

        private void Update()
		{
			if (playerConversant.IsActive())
			{
				if (playerConversant.HasNext() && !playerConversant.IsChoosing() && !playerConversant.GetTextIsRunning())
				{
					if (coroutineRunning != null)
					{
                        StopCoroutine(SeparatorDelay());
                    }

                    coroutineRunning = StartCoroutine(playerConversant.WriteTextWithDelay());
				} 
				else if ((playerConversant.IsChoosing() || !playerConversant.HasNext()) && !isSeparatorRunning)
				{
                    coroutineRunning = StartCoroutine(SeparatorDelay());
                }
            }

			if (bubbleRoot.childCount > maxNumberOfChilds && bubbleRoot.childCount > 0)
			{
				Destroy(bubbleRoot.GetChild(0).gameObject);
			}
        }
		private void UpdateChat()
		{
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
			AiWriterEffect.SetText(text);
            //AIText.text = text;
            Instantiate(prefabAibubble, bubbleRoot);
        }
		private void PlayerBubble(string text)
		{
			playerWriterEffect.SetText(text);
			//playerText.text = text;
			// Change to player when can talk
			Instantiate(separator, bubbleRoot);
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

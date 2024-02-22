using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using TMPro;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace UI
{
	public class DialogueUI : MonoBehaviour
	{
		[Header("General Configuration")]
		[SerializeField] private Transform bubbleRoot;
		private PlayerConversant playerConversant;
		
		[Header("Bubbles")]
		[SerializeField] private GameObject prefabAibubble;
        [SerializeField] private GameObject prefabPlayerbubble;
        [SerializeField] private GameObject separator;
		[Space(10)]
		[SerializeField] private float timerDelay = 2.0f;

        private TextMeshProUGUI playerText;
        private TextMeshProUGUI AIText;

		private bool isSeparatorRunning;

        private void Start()
		{
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			playerConversant.onConversationUpdated += UpdateChat;
		    
		    AIText = prefabAibubble.GetComponentInChildren<TextMeshProUGUI>();
		    playerText = prefabPlayerbubble.GetComponentInChildren<TextMeshProUGUI>();
		    
			DestroyChildrens(bubbleRoot);
		}
		
		private void Update()
		{
			if (playerConversant.IsActive())
			{
				if (playerConversant.HasNext() && !playerConversant.IsChoosing() && !playerConversant.GetTextIsRunning())
				{
					StartCoroutine(playerConversant.WriteTextWithDelay());
				} 
				else if ((playerConversant.IsChoosing() || !playerConversant.HasNext()) && !isSeparatorRunning)
				{
                    StartCoroutine(SeparatorDelay());
                }
            } 

        }
		private void UpdateChat()
		{
			if (!playerConversant.IsActive())
			{
				return;
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
				if (playerConversant.GetCanContinue())
				{
					StopAllCoroutines();
                    PlayerBubble(choice.GetText());
                    playerConversant.SelectChoice(choice);
                }
            }
        }
		private void AIBubble(string text)
		{
            AIText.text = text;
            Instantiate(prefabAibubble, bubbleRoot);
        }
		private void PlayerBubble(string text)
		{
			playerText.text = text;
			Instantiate(prefabPlayerbubble, bubbleRoot);
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

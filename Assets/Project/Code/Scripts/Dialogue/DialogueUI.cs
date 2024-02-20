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
		[SerializeField] private Transform bubbleRoot;
		private PlayerConversant playerConversant;
		
		[Header("Bubbles")]
		[SerializeField] private GameObject prefabAibubble;
        [SerializeField] private GameObject prefabPlayerbubble;
        [SerializeField] private GameObject separator;

        private TextMeshProUGUI playerText;
        private TextMeshProUGUI AIText;
	
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
				if (playerConversant.HasNext() && !playerConversant.IsChoosing())
				{
					StartCoroutine(playerConversant.WriteTextWithDelay());
				}
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
				// Clear current chat
			}
			
			if (!playerConversant.HasNext())
			{
                StartCoroutine(playerConversant.WriteTextWithDelay());
                Instantiate(separator, bubbleRoot);
            }

			if (playerConversant.IsChoosing())
			{
                PlayerChoosing();
            }
            else 
			{
				AIText.text = playerConversant.GetText();
				
				Instantiate(prefabAibubble, bubbleRoot);
			}
		}
	
		private void PlayerChoosing()
		{
			foreach (DialogueNode choice in playerConversant.GetChoices())
			{
                //Instantiate(separator, bubbleRoot);

            }
        }
		private void PlayerBubble(string text)
		{
			playerText.text = text;
			GameObject playerBubble = Instantiate(prefabPlayerbubble, bubbleRoot);
		}
		
		private void DestroyChildrens(Transform root)
		{
			foreach (Transform item in root)
			{
				Destroy(item.gameObject);
			}
		}

		protected void OnDestroy()
		{
			playerConversant.onConversationUpdated -= UpdateChat;
		}
	}
}

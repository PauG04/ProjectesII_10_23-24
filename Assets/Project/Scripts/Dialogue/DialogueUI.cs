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
		
		[Header("Conversant Name")]
		[SerializeField] private GameObject prefabNameLabel;
		private TextMeshProUGUI conversantName;
		
		[Header("AI Text")]
		[SerializeField] private GameObject prefabAibubble;
		private TextMeshProUGUI AIText;
		
		[Header("Player Text")]
		[SerializeField] private GameObject prefabPlayerbubble;
		private TextMeshProUGUI playerText;
		
		[Header("Choices")]
		[SerializeField] private Transform choiceRoot;
		[SerializeField] private GameObject choicePrefab;
	
		[Header("Buttons")]
		[SerializeField] private Button nextButton;
		[SerializeField] private Button quitButton;
	
		private void Start()
		{
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
			playerConversant.onConversationUpdated += UpdateChat;
		    
			//nextButton.onClick.AddListener(() => playerConversant.Next());
		    quitButton.onClick.AddListener(() => playerConversant.Quit());
		    
		    AIText = prefabAibubble.GetComponentInChildren<TextMeshProUGUI>();
		    playerText = prefabPlayerbubble.GetComponentInChildren<TextMeshProUGUI>();
			conversantName = prefabNameLabel.GetComponentInChildren<TextMeshProUGUI>();
		    
			DestroyChildrens(bubbleRoot);
			DestroyChildrens(choiceRoot);
			nextButton.gameObject.SetActive(false);
			//UpdateChat();
		    //UpdateUI();
		}
		
		private void Update()
		{
			if (playerConversant.IsActive())
			{
				if (playerConversant.HasNext() && !playerConversant.IsChoosing())
				{
					playerConversant.Next();
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
				conversantName.text = playerConversant.GetCurrentConversantName();
				GameObject nameLabel = Instantiate(prefabNameLabel, bubbleRoot);
			}
			
			choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

			if (playerConversant.IsChoosing())
			{
				BuildChoiceList();
			}
			else 
			{
				AIText.text = playerConversant.GetText();
				
				GameObject conversantBubble = Instantiate(prefabAibubble, bubbleRoot);
			}
			
		}
	
		private void BuildChoiceList()
		{
			DestroyChildrens(choiceRoot);
			
			foreach (DialogueNode choice in playerConversant.GetChoices())
			{
				GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
				
				TextMeshProUGUI textComponent =	choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
				textComponent.text = choice.GetText();
				
				Button button = choiceInstance.GetComponentInChildren<Button>();
				
				button.onClick.AddListener(() => 
				{
					PlayerBubble(choice.GetText());
					playerConversant.SelectChoice(choice);
				});
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

	}
}

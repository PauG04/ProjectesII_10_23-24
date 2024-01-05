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
		
		[Header("AI Text")]
		[SerializeField] private GameObject prefabAibubble;
		[SerializeField] private TextMeshProUGUI AIText;
		[SerializeField] private TextMeshProUGUI conversantName;
		[Space(10)]
		[SerializeField] private GameObject AIResponse;
		
		[Header("Player Text")]
		[SerializeField] private GameObject prefabPlayerbubble;
		[SerializeField] private TextMeshProUGUI playerText;
		
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
		    nextButton.onClick.AddListener(() => playerConversant.Next());
		    quitButton.onClick.AddListener(() => playerConversant.Quit());
		    
		    AIText = prefabAibubble.GetComponentInChildren<TextMeshProUGUI>();
		    playerText = prefabPlayerbubble.GetComponentInChildren<TextMeshProUGUI>();
		    
			bubbleRoot.DetachChildren();
			//UpdateChat();
		    //UpdateUI();
	    }
	
		private void UpdateUI()
		{
			gameObject.SetActive(playerConversant.IsActive());
			
			if(!playerConversant.IsActive())
			{
				return;
			}
			conversantName.text = playerConversant.GetCurrentConversantName();
			AIResponse.SetActive(!playerConversant.IsChoosing());
			choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
		    
			if (playerConversant.IsChoosing())
			{
				BuildChoiceList();
			}
			else 
			{
				AIText.text = playerConversant.GetText();
				nextButton.gameObject.SetActive(playerConversant.HasNext());
			}
		}
		
		private void UpdateChat()
		{
			GameObject conversantBubble = Instantiate(prefabAibubble, bubbleRoot);
			
			if (!playerConversant.IsActive())
			{
				return;
			}
			
			conversantName.text = playerConversant.GetCurrentConversantName();
			choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
			
			if (playerConversant.IsChoosing())
			{
				BuildChoiceList();
			}
			else 
			{
				AIText.text = playerConversant.GetText();
				nextButton.gameObject.SetActive(playerConversant.HasNext());
			}
		}
	
		private void BuildChoiceList()
		{
			choiceRoot.DetachChildren();
			foreach (DialogueNode choice in playerConversant.GetChoices())
			{
				GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
				TextMeshProUGUI textComponent =	choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
				textComponent.text = choice.GetText();
				Button button = choiceInstance.GetComponentInChildren<Button>();
				button.onClick.AddListener(() => 
				{
					playerConversant.SelectChoice(choice);
				});
			}
		}
	}
}

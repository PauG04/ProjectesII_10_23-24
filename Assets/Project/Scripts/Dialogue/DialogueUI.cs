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
		private PlayerConversant playerConversant;
		[SerializeField] private TextMeshProUGUI AIText;
		[SerializeField] private Button nextButton;
		[SerializeField] private GameObject AIResponse;
		
		[Header("Choices")]
		[SerializeField] private Transform choiceRoot;
		[SerializeField] private GameObject choicePrefab;
	
		private void Start()
	    {
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
		    nextButton.onClick.AddListener(Next);
		    
		    UpdateUI();
	    }
	
		private void Next()
		{
			playerConversant.Next();	
			UpdateUI();
		}
	
		private void UpdateUI()
		{
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
					UpdateUI();
				});
			}
		}
	}
}

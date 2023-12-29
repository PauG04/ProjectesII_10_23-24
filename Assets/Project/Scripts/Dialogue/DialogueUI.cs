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

		private void Start()
	    {
		    playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
		    nextButton.onClick.AddListener(Next);
		    
		    UpdateUI();
	    }
	
		void Next()
		{
			Debug.Log("NextPressed");
			playerConversant.Next();	
			UpdateUI();
		}
	
		void UpdateUI()
	    {
		    AIText.text = playerConversant.GetText();
		    nextButton.gameObject.SetActive(playerConversant.HasNext());
	    }
	}
}

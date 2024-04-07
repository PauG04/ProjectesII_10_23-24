using UnityEngine;
using Dialogue;

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

		[Header("Visual Components")]
        [SerializeField] private GameObject nextSprite;

        [Space(10)]	
		[SerializeField] private float timerDelay = 2.0f;
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
                playerConversant.SetIsTextDone(false);
                //TypeWriterEffect.CompleteTextRevealed -= Next;

                if (playerConversant.HasNext() && !playerConversant.IsChoosing())
				{
                    //TypeWriterEffect.CompleteTextRevealed += Next;
                    playerConversant.SetIsTextDone(true);

                    nextSprite.transform.eulerAngles = Vector3.zero;
                }
				else
				{
					//nextSprite.transform.eulerAngles = new Vector3(0, 0, 90f);
                }
            }

			if (bubbleRoot.childCount > maxNumberOfChilds && bubbleRoot.childCount > 0)
			{
				Destroy(bubbleRoot.GetChild(0).gameObject);
			}

			if (TypeWriterEffect.isTextCompleted && playerConversant.HasNext())
			{
				nextSprite.SetActive(true);
            }
			else
			{
				nextSprite.SetActive(false);
			}
        }
        private void UpdateChat()
		{
            if (!playerConversant.IsActive())
			{
				return;
			}
			if (playerConversant.IsNewConversant())
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
		public void DestroyAllBubbles()
		{
			DestroyChildrens(bubbleRoot);
		}
        protected void OnDestroy()
		{
			playerConversant.onConversationUpdated -= UpdateChat;
		}
	}
}

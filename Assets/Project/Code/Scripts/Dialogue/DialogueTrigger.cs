using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
namespace Dialogue
{
	public class DialogueTrigger : MonoBehaviour
	{
		[SerializeField] private string action;
		[SerializeField] private UnityEvent onTrigger;
		
		public void Trigger(string actionToTrigger)
		{
            if (actionToTrigger == action)
			{
                onTrigger.Invoke();
			}
		}

		public void SetTriggerAction(string action)
		{
			this.action = action;
		}

		public void SetOnTriggerEvent(UnityAction action)
		{
			onTrigger.AddListener(action);
		}
	}
}


using System;
using GameEventsSystem.Events;
using UnityEngine;

namespace DialogueSystem
{
	public class DialogueController : MonoBehaviour
	{
		public event Action<IDialogue> OnDialogue;
		 public FloatGameEvent Event;

		public Condition<float> floatCondition;
		public Condition<int> intCondition;
		public Condition<bool> boolCondition;
		
	}

	public interface IDialogue
	{
	}


	[System.Serializable]
	public class Condition<T>
	{
		public event Action OnCompletion;
		[SerializeReference] public GameEvent<T> Event;
		public T value;

		public void Initialize()
		{
			Event.OnRaise += Raised;
		}

		private void Raised(T obj)
		{
			if (obj.Equals(value))
			{
				OnCompletion?.Invoke();
			}
		}
	}
	
	
	
}
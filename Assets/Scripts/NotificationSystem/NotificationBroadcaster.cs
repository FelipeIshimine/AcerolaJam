using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotificationSystem
{
	public abstract class NotificationBroadcaster {}

	public class NotificationBroadcaster<T> : NotificationBroadcaster where T : INotification
	{
		private readonly List<NotificationListener<T>> listeners = new List<NotificationListener<T>>();
		public IReadOnlyList<NotificationListener<T>> Listeners => listeners;
		
		public void Raise(T value)
		{
			for (var index = 0; index < listeners.Count; index++)
			{
				var listener = listeners[index];
				listener.Raise(value);
				if (value.WasConsumed)
				{
					break;
				}
			}
		}

		public NotificationListener<T> Register(Action<T> callback, int priority)
		{
			NotificationListener<T> notificationListener = new NotificationListener<T>(callback, priority);
			var index = listeners.BinarySearch(notificationListener);
			if (index < 0)
			{
				index = ~index;
			}
			listeners.Insert(index,notificationListener);
			return notificationListener;
		}

		public bool TryUnregister(NotificationListener<T> notificationListener)
		{
			var index = listeners.BinarySearch(notificationListener);
			if (index < 0)
			{
				Debug.LogWarning("Listener not found");
				return false;
			}
			listeners.RemoveAt(index);
			return true;
		}
		
		public virtual Type GetParameterType() => typeof(T);

	}

	public class NotificationBroadcaster<T, TB> : NotificationBroadcaster<TB> where T : TB where TB : INotification
	{
		public override Type GetParameterType() => typeof(T);

	}
}
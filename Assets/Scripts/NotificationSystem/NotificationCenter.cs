using System;
using System.Collections.Generic;

namespace NotificationSystem
{
	public class NotificationCenter<T> : IReadOnlyNotificationCenter<T> where T : INotification
	{
		private readonly Dictionary<Type, NotificationBroadcaster<T>> broadcasters = new Dictionary<Type, NotificationBroadcaster<T>>();
		private readonly NotificationBroadcaster<T> anyBroadcaster = new NotificationBroadcaster<T>();


		public NotificationListener<T> RegisterToAny(Action<T> callback, int priority)
		{
			return anyBroadcaster.Register(callback, priority);
		}

		public bool TryUnregisterFromAny(NotificationListener<T> listener)
		{
			return anyBroadcaster.TryUnregister(listener);
		}

		public NotificationListener<T> Register(Action<T> callback, int priority)
		{
			var type = typeof(T);
			//Debug.Log($"Registering to type:{type.Name} / {typeof(TB).Name}");
			if (!broadcasters.TryGetValue(type, out var broadcaster))
			{
				broadcasters[type] = broadcaster = new NotificationBroadcaster<T>();
			}
			var listener = broadcaster.Register(callback.Invoke, priority);
			return listener;
		}

		public bool TryUnregister(NotificationListener<T> listener)
		{
			var type = listener.GetParameterType();
			if (broadcasters.TryGetValue(type, out var broadcaster))
			{
				broadcaster.TryUnregister(listener);
				return true;
			}
			//Debug.LogWarning($"Broadcaster for type:{type.Name} not found");
            return false;
		}

		public void Raise<TB>(TB value) where TB : T
		{
			//Debug.Log($"Raise:{value.GetType().Name} / {typeof(TB).Name}");
			if (broadcasters.TryGetValue(value.GetType(), out var broadcaster))
			{
				broadcaster.Raise(value);
			}
			else
			{
				//Debug.Log($"No broadcaster found for:{value.GetType().Name} / {typeof(TB).Name}");
			}
			anyBroadcaster.Raise(value);
		}
	}
	

	public interface IReadOnlyNotificationCenter<T> where T : INotification
	{
		/// <summary>
		/// </summary>
		/// <param name="callback"></param>
		/// <param name="priority">Lower First</param>
		/// <typeparam name="TB"></typeparam>
		/// <returns></returns>
		NotificationListener<T> Register(Action<T> callback, int priority);
		NotificationListener<T> RegisterToAny(Action<T> callback, int priority);
		bool TryUnregisterFromAny(NotificationListener<T> listener);
		bool TryUnregister(NotificationListener<T> listener);
	}
}
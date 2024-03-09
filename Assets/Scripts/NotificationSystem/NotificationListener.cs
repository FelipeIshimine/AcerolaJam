using System;

namespace NotificationSystem
{
	public class NotificationListener : IComparable<NotificationListener>
	{
		public readonly int Priority;
		protected NotificationListener(int priority) => Priority = priority;

		public int CompareTo(NotificationListener other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return Priority.CompareTo(other.Priority);
		}
	}
	
	public class NotificationListener<T> : NotificationListener
	{
		private event Action<T> Callback;

		public NotificationListener(Action<T> callback, int priority) : base(priority)
		{
			this.Callback = callback;
		}

		public void Raise(T value) => Callback?.Invoke(value);

		public virtual Type GetParameterType() => typeof(T);


	}
}
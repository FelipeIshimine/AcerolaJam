using System;

namespace VariableReferences
{
	[System.Serializable]
	public abstract class VariableReference<T>
	{
		public event Action<T> OnValueChanged;

		public T Value
		{
			get => GetValue();
			set
			{
				if (!value.Equals(GetValue()))
				{
					SetValue(value);
					OnValueChanged?.Invoke(GetValue());
				}
			}
		}

		protected abstract T GetValue();
		protected abstract void SetValue(T newValue);
		
		public static implicit operator T(VariableReference<T> source) => source.Value;
	}
}

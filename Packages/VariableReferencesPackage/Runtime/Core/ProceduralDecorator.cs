namespace VariableReferences
{
	[System.Serializable]
	public abstract class ProceduralDecorator<T>
	{
		public abstract T Process(T inValue);
	}
}
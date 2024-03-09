using UnityEngine;

namespace VariableReferences.Decorators
{
	[System.Serializable]
	public class RandomRangeFloatDecorator : FloatDecorator, ISerializationCallbackReceiver
	{
		[SerializeField] private Vector2 range;
		
		public override float Process(float inValue) => inValue + Random.Range(range.x, range.y);

		public void OnBeforeSerialize()
		{
			range.y = Mathf.Max(range.y, range.x);
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
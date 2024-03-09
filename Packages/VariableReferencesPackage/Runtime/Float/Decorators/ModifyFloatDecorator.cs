using UnityEngine;

namespace VariableReferences.Decorators
{
	[System.Serializable]
	public class ModifyFloatDecorator : FloatDecorator
	{
		[SerializeField] private float modification;
		public override float Process(float inValue) => inValue + modification;
	}
}
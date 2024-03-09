using UnityEngine;

namespace VariableReferences.Extras
{
	[System.Serializable]
	public class LightIntensityFloatReference : FloatReference
	{
		[SerializeField] private Light target;
		protected override float GetValue() => target.intensity;
		protected override void SetValue(float newValue) => target.intensity = newValue;
	}
}
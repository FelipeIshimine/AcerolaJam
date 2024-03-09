using UnityEngine;

namespace VariableReferences.Extras
{
	[System.Serializable]
	public class AudioSourceVolumeFloatReference : FloatReference
	{
		[SerializeField] private AudioSource target;
		protected override float GetValue() => target.volume;
		protected override void SetValue(float newValue) => target.volume = newValue;
	}
}
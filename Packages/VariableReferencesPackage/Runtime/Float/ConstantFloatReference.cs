using UnityEngine;

[System.Serializable]
public class ConstantFloatReference : FloatReference 
{
	[SerializeField] private float target;
    protected override float GetValue() => target;
    protected override void SetValue(float newValue) => target = newValue;
}

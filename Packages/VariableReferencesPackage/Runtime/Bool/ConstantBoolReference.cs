using UnityEngine;

[System.Serializable]
public class ConstantBoolReference : BoolReference 
{
	[SerializeField] private bool target;
    protected override bool GetValue() => target;
    protected override void SetValue(bool newValue) => target = newValue;
}

using UnityEngine;

[System.Serializable]
public class ConstantIntReference : IntReference 
{
	[SerializeField] private int target;
    protected override int GetValue() => target;
    protected override void SetValue(int newValue) => target = newValue;
}

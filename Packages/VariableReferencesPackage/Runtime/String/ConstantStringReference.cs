using UnityEngine;

[System.Serializable]
public class ConstantStringReference : StringReference 
{
	[SerializeField] private string target;
    protected override string GetValue() => target;
    protected override void SetValue(string newValue) => target = newValue;
}

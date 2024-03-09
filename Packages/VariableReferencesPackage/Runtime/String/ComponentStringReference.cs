using UnityEngine;

[System.Serializable]
public class ComponentStringReference : StringReference 
{
	[SerializeField] private StringComponent target;
    
    protected override string GetValue() => target.Value;
    protected override void SetValue(string newValue) => target.Value = newValue;
        
}

using UnityEngine;

[System.Serializable]
public class ComponentIntReference : IntReference 
{
	[SerializeField] private IntComponent target;
    
    protected override int GetValue() => target.Value;
    protected override void SetValue(int newValue) => target.Value = newValue;
        
}

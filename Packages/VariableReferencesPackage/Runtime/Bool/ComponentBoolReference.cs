using UnityEngine;

[System.Serializable]
public class ComponentBoolReference : BoolReference 
{
	[SerializeField] private BoolComponent target;
    
    protected override bool GetValue() => target.Value;
    protected override void SetValue(bool newValue) => target.Value = newValue;
        
}

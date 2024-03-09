using UnityEngine;

[System.Serializable]
public class ComponentFloatReference : FloatReference 
{
	[SerializeField] private FloatComponent target;
    
    protected override float GetValue() => target.Value;
    protected override void SetValue(float newValue) => target.Value = newValue;
        
}

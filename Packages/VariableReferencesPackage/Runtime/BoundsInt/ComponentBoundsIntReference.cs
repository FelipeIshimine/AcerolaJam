using UnityEngine;

[System.Serializable]
public class ComponentBoundsIntReference : BoundsIntReference 
{
	[SerializeField] private BoundsIntComponent target;
    
    protected override BoundsInt GetValue() => target.Value;
    protected override void SetValue(BoundsInt newValue) => target.Value = newValue;
        
}

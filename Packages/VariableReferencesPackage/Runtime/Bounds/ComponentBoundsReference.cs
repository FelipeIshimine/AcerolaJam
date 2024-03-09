using UnityEngine;

[System.Serializable]
public class ComponentBoundsReference : BoundsReference 
{
	[SerializeField] private BoundsComponent target;
    
    protected override Bounds GetValue() => target.Value;
    protected override void SetValue(Bounds newValue) => target.Value = newValue;
        
}

using UnityEngine;

[System.Serializable]
public class ConstantBoundsIntReference : BoundsIntReference 
{
	[SerializeField] private BoundsInt target;
    protected override BoundsInt GetValue() => target;
    protected override void SetValue(BoundsInt newValue) => target = newValue;
}

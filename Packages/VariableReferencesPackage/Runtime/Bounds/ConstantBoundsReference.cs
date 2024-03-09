using UnityEngine;

[System.Serializable]
public class ConstantBoundsReference : BoundsReference 
{
	[SerializeField] private Bounds target;
    protected override Bounds GetValue() => target;
    protected override void SetValue(Bounds newValue) => target = newValue;
}

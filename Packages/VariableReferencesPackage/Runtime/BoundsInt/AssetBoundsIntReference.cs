using UnityEngine;

[System.Serializable]
public class AssetBoundsIntReference : BoundsIntReference
{
	[SerializeField] private BoundsIntAsset target;
    protected override BoundsInt GetValue() => target.Value;
    protected override void SetValue(BoundsInt newValue) => target.Value = newValue;
}

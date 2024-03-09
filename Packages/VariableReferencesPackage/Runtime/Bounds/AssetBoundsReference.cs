using UnityEngine;

[System.Serializable]
public class AssetBoundsReference : BoundsReference
{
	[SerializeField] private BoundsAsset target;
    protected override Bounds GetValue() => target.Value;
    protected override void SetValue(Bounds newValue) => target.Value = newValue;
}

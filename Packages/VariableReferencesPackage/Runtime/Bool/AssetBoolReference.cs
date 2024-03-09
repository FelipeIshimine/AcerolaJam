using UnityEngine;

[System.Serializable]
public class AssetBoolReference : BoolReference
{
	[SerializeField] private BoolAsset target;
    protected override bool GetValue() => target.Value;
    protected override void SetValue(bool newValue) => target.Value = newValue;
}

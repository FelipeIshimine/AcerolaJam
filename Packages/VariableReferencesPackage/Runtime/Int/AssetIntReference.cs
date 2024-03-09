using UnityEngine;

[System.Serializable]
public class AssetIntReference : IntReference
{
	[SerializeField] private IntAsset target;
    protected override int GetValue() => target.Value;
    protected override void SetValue(int newValue) => target.Value = newValue;
}

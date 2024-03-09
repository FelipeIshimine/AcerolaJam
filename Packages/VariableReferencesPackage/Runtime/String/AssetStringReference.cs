using UnityEngine;

[System.Serializable]
public class AssetStringReference : StringReference
{
	[SerializeField] private StringAsset target;
    protected override string GetValue() => target.Value;
    protected override void SetValue(string newValue) => target.Value = newValue;
}

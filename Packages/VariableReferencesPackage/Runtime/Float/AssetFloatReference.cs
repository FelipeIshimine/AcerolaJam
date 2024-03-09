using UnityEngine;

[System.Serializable]
public class AssetFloatReference : FloatReference
{
	[SerializeField] private FloatAsset target;
    protected override float GetValue() => target.Value;
    protected override void SetValue(float newValue) => target.Value = newValue;
}

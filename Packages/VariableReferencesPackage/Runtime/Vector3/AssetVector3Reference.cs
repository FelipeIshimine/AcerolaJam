using UnityEngine;

[System.Serializable]
public class AssetVector3Reference : Vector3Reference
{
	[SerializeField] private Vector3Asset target;
    protected override Vector3 GetValue() => target.Value;
    protected override void SetValue(Vector3 newValue) => target.Value = newValue;
}

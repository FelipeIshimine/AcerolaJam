using UnityEngine;

[System.Serializable]
public class AssetVector3IntReference : Vector3IntReference
{
	[SerializeField] private Vector3IntAsset target;
    protected override Vector3Int GetValue() => target.Value;
    protected override void SetValue(Vector3Int newValue) => target.Value = newValue;
}

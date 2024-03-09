using UnityEngine;

[System.Serializable]
public class AssetVector2Reference : Vector2Reference
{
	[SerializeField] private Vector2Asset target;
    protected override Vector2 GetValue() => target.Value;
    protected override void SetValue(Vector2 newValue) => target.Value = newValue;
}

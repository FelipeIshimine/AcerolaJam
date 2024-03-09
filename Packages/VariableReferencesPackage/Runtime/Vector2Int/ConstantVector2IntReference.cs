using UnityEngine;

[System.Serializable]
public class ConstantVector2IntReference : Vector2IntReference 
{
	[SerializeField] private Vector2Int target;
    protected override Vector2Int GetValue() => target;
    protected override void SetValue(Vector2Int newValue) => target = newValue;
}

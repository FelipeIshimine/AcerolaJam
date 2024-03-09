using UnityEngine;

[System.Serializable]
public class ConstantVector2Reference : Vector2Reference 
{
	[SerializeField] private Vector2 target;
    protected override Vector2 GetValue() => target;
    protected override void SetValue(Vector2 newValue) => target = newValue;
}

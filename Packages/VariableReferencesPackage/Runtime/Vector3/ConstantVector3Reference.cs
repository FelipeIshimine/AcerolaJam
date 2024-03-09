using UnityEngine;

[System.Serializable]
public class ConstantVector3Reference : Vector3Reference 
{
	[SerializeField] private Vector3 target;
    protected override Vector3 GetValue() => target;
    protected override void SetValue(Vector3 newValue) => target = newValue;
}

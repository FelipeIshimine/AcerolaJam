using UnityEngine;

[System.Serializable]
public class ConstantVector3IntReference : Vector3IntReference 
{
	[SerializeField] private Vector3Int target;
    protected override Vector3Int GetValue() => target;
    protected override void SetValue(Vector3Int newValue) => target = newValue;
}

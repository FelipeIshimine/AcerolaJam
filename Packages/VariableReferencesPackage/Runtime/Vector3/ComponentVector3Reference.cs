using UnityEngine;

[System.Serializable]
public class ComponentVector3Reference : Vector3Reference 
{
	[SerializeField] private Vector3Component target;
    
    protected override Vector3 GetValue() => target.Value;
    protected override void SetValue(Vector3 newValue) => target.Value = newValue;
        
}

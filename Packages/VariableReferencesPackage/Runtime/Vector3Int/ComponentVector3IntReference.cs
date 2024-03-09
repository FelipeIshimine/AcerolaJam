using UnityEngine;

[System.Serializable]
public class ComponentVector3IntReference : Vector3IntReference 
{
	[SerializeField] private Vector3IntComponent target;
    
    protected override Vector3Int GetValue() => target.Value;
    protected override void SetValue(Vector3Int newValue) => target.Value = newValue;
        
}

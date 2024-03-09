using UnityEngine;

[System.Serializable]
public class ComponentVector2IntReference : Vector2IntReference 
{
	[SerializeField] private Vector2IntComponent target;
    
    protected override Vector2Int GetValue() => target.Value;
    protected override void SetValue(Vector2Int newValue) => target.Value = newValue;
        
}

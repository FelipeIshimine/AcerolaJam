using UnityEngine;

[System.Serializable]
public class ComponentVector2Reference : Vector2Reference 
{
	[SerializeField] private Vector2Component target;
    
    protected override Vector2 GetValue() => target.Value;
    protected override void SetValue(Vector2 newValue) => target.Value = newValue;
        
}

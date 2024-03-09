using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector2Reference : Vector2Reference
{
    [SerializeField] private ProceduralVector2 proceduralVariable;
    protected override Vector2 GetValue() => proceduralVariable.Get();
    protected override void SetValue(Vector2 newValue) => proceduralVariable.Set(newValue);
}
using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector2IntReference : Vector2IntReference
{
    [SerializeField] private ProceduralVector2Int proceduralVariable;
    protected override Vector2Int GetValue() => proceduralVariable.Get();
    protected override void SetValue(Vector2Int newValue) => proceduralVariable.Set(newValue);
}
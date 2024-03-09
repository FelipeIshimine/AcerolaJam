using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector3IntReference : Vector3IntReference
{
    [SerializeField] private ProceduralVector3Int proceduralVariable;
    protected override Vector3Int GetValue() => proceduralVariable.Get();
    protected override void SetValue(Vector3Int newValue) => proceduralVariable.Set(newValue);
}
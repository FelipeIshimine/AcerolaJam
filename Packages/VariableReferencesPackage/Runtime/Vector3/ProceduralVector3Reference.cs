using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector3Reference : Vector3Reference
{
    [SerializeField] private ProceduralVector3 proceduralVariable;
    protected override Vector3 GetValue() => proceduralVariable.Get();
    protected override void SetValue(Vector3 newValue) => proceduralVariable.Set(newValue);
}
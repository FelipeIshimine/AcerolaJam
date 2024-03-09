using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralIntReference : IntReference
{
    [SerializeField] private ProceduralInt proceduralVariable;
    protected override int GetValue() => proceduralVariable.Get();
    protected override void SetValue(int newValue) => proceduralVariable.Set(newValue);
}
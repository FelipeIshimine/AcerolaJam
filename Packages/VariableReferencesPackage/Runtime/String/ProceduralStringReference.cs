using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralStringReference : StringReference
{
    [SerializeField] private ProceduralString proceduralVariable;
    protected override string GetValue() => proceduralVariable.Get();
    protected override void SetValue(string newValue) => proceduralVariable.Set(newValue);
}
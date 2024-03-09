using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralBoolReference : BoolReference
{
    [SerializeField] private ProceduralBool proceduralVariable;
    protected override bool GetValue() => proceduralVariable.Get();
    protected override void SetValue(bool newValue) => proceduralVariable.Set(newValue);
}
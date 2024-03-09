using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralBoundsIntReference : BoundsIntReference
{
    [SerializeField] private ProceduralBoundsInt proceduralVariable;
    protected override BoundsInt GetValue() => proceduralVariable.Get();
    protected override void SetValue(BoundsInt newValue) => proceduralVariable.Set(newValue);
}
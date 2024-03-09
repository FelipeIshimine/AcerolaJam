using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralBoundsReference : BoundsReference
{
    [SerializeField] private ProceduralBounds proceduralVariable;
    protected override Bounds GetValue() => proceduralVariable.Get();
    protected override void SetValue(Bounds newValue) => proceduralVariable.Set(newValue);
}
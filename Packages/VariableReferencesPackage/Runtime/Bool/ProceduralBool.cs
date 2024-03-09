using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralBool : ProceduralVariable<bool, BoolReference, BoolDecorator>
{
    public ProceduralBool()
	{
		baseValue = new ConstantBoolReference();
	}
}

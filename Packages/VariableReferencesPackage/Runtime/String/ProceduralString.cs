using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralString : ProceduralVariable<string, StringReference, StringDecorator>
{
    public ProceduralString()
	{
		baseValue = new ConstantStringReference();
	}
}

using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralInt : ProceduralVariable<int, IntReference, IntDecorator>
{
    public ProceduralInt()
	{
		baseValue = new ConstantIntReference();
	}
}

using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralFloat : ProceduralVariable<float, FloatReference, FloatDecorator>
{
    public ProceduralFloat()
	{
		baseValue = new ConstantFloatReference();
	}
}

using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector3 : ProceduralVariable<Vector3, Vector3Reference, Vector3Decorator>
{
    public ProceduralVector3()
	{
		baseValue = new ConstantVector3Reference();
	}
}

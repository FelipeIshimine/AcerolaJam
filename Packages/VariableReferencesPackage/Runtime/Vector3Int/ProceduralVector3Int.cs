using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector3Int : ProceduralVariable<Vector3Int, Vector3IntReference, Vector3IntDecorator>
{
    public ProceduralVector3Int()
	{
		baseValue = new ConstantVector3IntReference();
	}
}

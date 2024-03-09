using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector2Int : ProceduralVariable<Vector2Int, Vector2IntReference, Vector2IntDecorator>
{
    public ProceduralVector2Int()
	{
		baseValue = new ConstantVector2IntReference();
	}
}

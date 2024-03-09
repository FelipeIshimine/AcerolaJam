using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralVector2 : ProceduralVariable<Vector2, Vector2Reference, Vector2Decorator>
{
    public ProceduralVector2()
	{
		baseValue = new ConstantVector2Reference();
	}
}

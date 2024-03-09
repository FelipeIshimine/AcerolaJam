using UnityEditor;

namespace VariableReferences.Editor
{
	[CustomPropertyDrawer(typeof(ProceduralDecorator<>), true)]
	public class ProceduralDecoratorDrawer : SerializeReferenceDropdownDrawer 
	{
	}
}
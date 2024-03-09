using UnityEditor;

namespace VariableReferences.Editor
{
	[CustomPropertyDrawer(typeof(VariableReference<>), true)]
	public class VariableReferenceDrawer : SerializeReferenceDropdownDrawer
	{
	}
}

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VariableReferences.Editor
{
	/*[CustomPropertyDrawer(typeof(ProceduralVariable<,,>), true)]
	public class ProceduralVariableDrawer : PropertyDrawer 
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			Foldout container = new Foldout()
			{
				text = property.displayName
			};
			
			container.Add(new PropertyField(property.FindPropertyRelative("baseValue")));
			container.Add(new PropertyField(property.FindPropertyRelative("decorators")));
			
			return container;
		}
	}*/
}
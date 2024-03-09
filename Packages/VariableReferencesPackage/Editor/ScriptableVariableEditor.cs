using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace VariableReferences.Editor
{
	[CustomEditor(typeof(ScriptableVariable<>),true)]
	public class ScriptableVariableEditor : UnityEditor.Editor
	{
		private UnityEditor.Editor editor;

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement element = new VisualElement();
			SerializedObject serializedObject = new SerializedObject(target);
			var defaultValue = serializedObject.FindProperty("defaultValue");
			var defaultPropField = new PropertyField(defaultValue);
			element.Add(defaultPropField);
		
			if (Application.isPlaying)
			{
				defaultPropField.SetEnabled(false);
				var liveValue = serializedObject.FindProperty("liveValue");
				var livePropField = new PropertyField(liveValue);
				element.Add(livePropField);
			}

			return element;
		}
	}
}
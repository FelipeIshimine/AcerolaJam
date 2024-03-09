using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
	[CustomEditor(typeof(DialogueController))]
	public class DialogueControllerEditor : UnityEditor.Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement container = new VisualElement();
			var iterator = serializedObject.GetIterator();

			iterator.NextVisible(true);
			do
			{
				container.Add(new PropertyField(iterator));
			} while (iterator.NextVisible(false));
			
			return container;
		}
	}
}
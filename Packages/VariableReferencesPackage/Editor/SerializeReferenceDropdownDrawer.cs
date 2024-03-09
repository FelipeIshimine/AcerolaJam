using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VariableReferences.Attributes;

namespace VariableReferences.Editor
{
	
	//[CustomPropertyDrawer(typeof(SerializeReferenceDropdownAttribute))]
	public class SerializeReferenceDropdownDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			if (SerializationUtility.HasManagedReferencesWithMissingTypes(property.serializedObject.targetObject))
			{
				SerializationUtility.ClearAllManagedReferencesWithMissingTypes(property.serializedObject.targetObject);
			}

			var container = new VisualElement()
			{
				style = { flexDirection = FlexDirection.Row }
			};
			CreatePropertyField(container, property);
			return container;
		}

		
		private void CreatePropertyField(VisualElement propertyContainer, SerializedProperty property)
		{
			var propertyField = new PropertyField(property, property.displayName)
			{
				style = { flexGrow = 1 }
			};
			propertyField.name = "Field";
			
			EditorToolbarDropdown typeBtn = null;
			typeBtn = new EditorToolbarDropdown(GetButtonLabel(property), () => TypeDropdownClicked(typeBtn, property))
			{
				style =
				{
					height = 14,
					position = Position.Absolute,
					left = new StyleLength(StyleKeyword.Auto),
					right = 0
				}
			};
			propertyContainer.Add(propertyField);
			propertyContainer.Add(typeBtn);
		}

		private string GetButtonLabel(SerializedProperty p)
		{
			return p.managedReferenceValue != null
				? GetDisplayName(p.managedReferenceValue.GetType()).Replace(p.managedReferenceValue.GetType().BaseType.Name, string.Empty)
				: "-Select Type-";
		}

		private string GetDisplayName(Type type) => type.FullName.Replace(type.Namespace,string.Empty).Replace(".",string.Empty);

		private void TypeDropdownClicked(EditorToolbarDropdown typeBtn, SerializedProperty property)
		{
			Type targetType;
			if (fieldInfo.FieldType.IsConstructedGenericType)
			{
				targetType = fieldInfo.FieldType.GetGenericArguments()[0];
			}   
			else
			{
				targetType = fieldInfo.FieldType;
			}


			var types = new List<Type>();
			
			foreach (var type in TypeCache.GetTypesDerivedFrom(targetType))
			{
				if (!type.IsAbstract)
				{
					types.Add(type);
				}
			}

			var baseTypeName = GetDisplayName(targetType);
			var typesArray = types.ToArray();

			string[] labels = new string [typesArray.Length];

			for (int i = 0; i < labels.Length; i++)
			{
				var type = typesArray[i];

				string path = null;

				foreach (object customAttribute in type.GetCustomAttributes(typeof(DropdownPathAttribute), false))
				{
					if (customAttribute is DropdownPathAttribute dropdownPathAttribute)
					{
						path = dropdownPathAttribute.Path;
					}
				}

				if (string.IsNullOrEmpty(path))
				{
					labels[i] = GetDisplayName(type).Replace(baseTypeName, string.Empty);
				}
				else
				{
					labels[i] = path;
				}
			}

			GenericAdvanceDropdown dropdown =
				new GenericAdvanceDropdown(
					labels,
					index => OnTypeSelected(index, typeBtn, property, typesArray));

			dropdown.Show(typeBtn.worldBound);
		}

		void OnTypeSelected(int index, EditorToolbarDropdown typeBtn, SerializedProperty property, Type[] typesArray)
		{
			property.managedReferenceValue = Activator.CreateInstance(typesArray[index]);
			property.serializedObject.ApplyModifiedProperties();
			typeBtn.text = GetButtonLabel(property);

			/*bool isProcedural = property.managedReferenceValue != null &&
				property.managedReferenceValue.GetType().Name.Contains("Procedural");
			var p = property.Copy();
			if (p.NextVisible(true))
			{
				typeBtn.parent.Q<PropertyField>().bindingPath = p.propertyPath;
			}
			else
			{
				typeBtn.parent.Q<PropertyField>().bindingPath = property.propertyPath;
			}
			typeBtn.style.position = (isProcedural)?Position.Absolute:Position.Relative;
			*/
			
			
			typeBtn.parent.Q<PropertyField>().Bind(property.serializedObject);

		}
		
		static bool IsSubclassOfRawGeneric(Type generic, Type toCheck) {
			while (toCheck != null && toCheck != typeof(object)) 
			{
				var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == cur) 
				{
					return true;
				}
				toCheck = toCheck.BaseType;
			}
			return false;
		}
	}
	
}


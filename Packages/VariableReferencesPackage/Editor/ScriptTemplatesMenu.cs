using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace VariableReferences.Editor
{
	public class ScriptTemplateMenu : EditorWindow
	{
		public static string[] TemplateFileNames = new[]
		{
			"AssetVariableReferenceTemplate.txt",
			"ComponentVariableReferenceTemplate.txt",
			"ConstantVariableReferenceTemplate.txt",
			"ProceduralVariableReferenceTemplate.txt",
			"ProceduralVariableTemplate.txt",
			"VariableAssetTemplate.txt",
			"VariableComponentTemplate.txt",
			"VariableDecoratorTemplate.txt",
			"VariableReferenceTemplate.txt",
		};

		
		[MenuItem("Assets/Create Script Templates")]
		public static void CreateScriptTemplates()
		{
			ScriptTemplateMenu window = ScriptableObject.CreateInstance<ScriptTemplateMenu>();
			//window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
			window.name = "Create VariableReference Types";
			window.ShowUtility();
		}

		private void CreateGUI()
		{
			var textField = new TextField("Name:")
			{
				isDelayed = true
			};
			rootVisualElement.Add(textField);
			rootVisualElement.style.flexShrink = 1;
			textField.Focus();
			textField.RegisterValueChangedCallback(OnTextSet);
		}

		private void OnTextSet(ChangeEvent<string> evt)
		{
			if (!string.IsNullOrEmpty(evt.newValue))
			{
				CreateScriptTemplates(evt.newValue);
			}
			Close();
		}

		private static void CreateScriptTemplates(string name)
		{
			Object[] selectedObjects = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
			if (selectedObjects.Length >= 1)
			{
				string folderPath = AssetDatabase.GetAssetPath(selectedObjects[0]);
				Debug.Log(folderPath);
				var directoryName = Path.GetDirectoryName(folderPath);

				if (!AssetDatabase.IsValidFolder(folderPath))
				{
					folderPath = Path.GetDirectoryName(folderPath);
				}

				var titleName = string.Concat(name[0].ToString().ToUpper(), name[1..]);

				if (!AssetDatabase.IsValidFolder($"{folderPath}/{titleName}"))
				{
					AssetDatabase.CreateFolder(folderPath, titleName);
				}

				folderPath += $"/{titleName}";
				foreach (string templateFileName in TemplateFileNames)
				{
					var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(
						$"Packages/ishimine.variable-references/Editor/ScriptTemplates/{templateFileName}");
					string templateText = textAsset.text;
					string newName = $"{Path.GetFileNameWithoutExtension(templateFileName)}.cs";
					newName = newName.Replace("Variable", titleName);
					newName = newName.Replace("Template", string.Empty);

					string newPath = Application.dataPath.Replace("Assets", string.Empty) +
						Path.Combine(folderPath, newName);
					Debug.Log($"NewName:{newName} newPath:{newPath}");
					File.WriteAllText(newPath, string.Format(templateText, titleName, name));
				}
				AssetDatabase.Refresh();
			}
			
		}
	}
}
using System;
using System.IO;
using Common.Editor.ToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ToolbarExtensions
{
	[InitializeOnLoad]
	public class ToolbarExtensionsLoader
	{
		private static EditorToolbarDropdown additiveDropdown;
		private static EditorToolbarDropdown singleDropdown;

		static ToolbarExtensionsLoader()
		{
			ToolbarExtender.LeftToolbarGUI.Add(CreateLeftVisualElement());
			ToolbarExtender.RightToolbarGUI.Add(CreateRightVisualElement());
		}

		static VisualElement CreateLeftVisualElement()
		{
			var style = (StyleSheet)EditorGUIUtility.Load("StyleSheets/GraphView/GraphView.uss");
		
			VisualElement container = new VisualElement();
			container.style.flexDirection = FlexDirection.Row;
			container.styleSheets.Add(style);
		
			if (Application.isPlaying)
			{
				container.Add(new ToolbarButton(()=>
				{
					if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
					{
						EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
						EditorApplication.EnterPlaymode();
					}
				})
				{
					text = "RESTART",
					style = { alignContent = Align.Center }
				});
			}
			else
			{
				
				/*container.Add(new ToolbarButton(()=>
				{
					Debug.Log(EditorBuildSettings.scenes[0].path);
					EditorSceneManager.OpenScene("Assets/Scenes/AnimationsPreview Scene.unity");
					EditorApplication.EnterPlaymode();
				})
				{
					text = "Animation Previews",
					style = { alignContent = Align.Center }
				});*/
				
				container.Add(new ToolbarButton(()=>
				{
					EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
					EditorApplication.EnterPlaymode();
				})
				{
					text = "PLAY",
					style = { alignContent = Align.Center }
				});
			}

			return container;
		}
		
		static VisualElement CreateRightVisualElement()
		{
			var style = (StyleSheet)EditorGUIUtility.Load("StyleSheets/GraphView/GraphView.uss");
			//var style=AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/ToolbarExtensions/ToolbarExtensionsStyle.uss");
			VisualElement container = new VisualElement();
			container.style.flexDirection = FlexDirection.Row;
			container.styleSheets.Add(style);
	
			if (Application.isPlaying)
			{
				return container;
			}
            
			//container.Add(new Label("Scenes:"));
			additiveDropdown = new EditorToolbarDropdown("Open Scene", SingleDropdownClicked);
			container.Add(additiveDropdown);
			
			singleDropdown = new EditorToolbarDropdown("+", AdditiveDropdownClicked);
			container.Add(singleDropdown);
			return container;
		}

		static void SingleDropdownClicked()
		{
			var guids = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}", new string[]{"Assets"});

			var paths = Array.ConvertAll(guids,AssetDatabase.GUIDToAssetPath);
			var names = Array.ConvertAll(paths, Path.GetFileName);
			
			void LoadScene(int index) => EditorSceneManager.OpenScene(paths[index], OpenSceneMode.Single);

			QuickAdvancedDropdown dropdown = new QuickAdvancedDropdown(names,LoadScene);
			dropdown.Show(singleDropdown.worldBound);
		}
		
		static void AdditiveDropdownClicked()
		{
			var guids = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}", new string[]{"Assets"});

			var paths = Array.ConvertAll(guids,AssetDatabase.GUIDToAssetPath);
			var names = Array.ConvertAll(paths, Path.GetFileName);
			
			void LoadScene(int index) => EditorSceneManager.OpenScene(paths[index], OpenSceneMode.Additive);

			QuickAdvancedDropdown dropdown = new QuickAdvancedDropdown(names,LoadScene);
			dropdown.Show(additiveDropdown.worldBound);
		}

	


	}
}
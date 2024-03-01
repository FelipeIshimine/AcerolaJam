using System.Threading.Tasks;
using Controllers.GameplayState;
using Controllers.MenuState;
using Cysharp.Threading.Tasks;
using Models.GameFlow;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Controllers
{
	public class GameFlow : MonoBehaviour
	{
		[SerializeField] private AssetReference menuScene;
		[SerializeField] private AssetReference gameplayScene;

		public async UniTaskVoid Start()
		{
			DontDestroyOnLoad(gameObject);
			GameFlowResult result = new GoToMenuResult();

			do
			{
				switch (result)
				{
					case GoToMenuResult:
						result = await GoToMenu();
						break;
					case GoToGameplayResult:
						result = await GoToGameplay();
						break;
				}
			} while (result is not QuitResult);
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}

		private async Task<GameFlowResult> GoToGameplay()
		{
			Debug.Log("GoToGameplay");
			var sceneHandler = gameplayScene.LoadSceneAsync(LoadSceneMode.Single);
			var sceneInstance = await sceneHandler;

			var result = await FindObjectOfType<GameplayController>().RunAsync();

			return result;
		}

		private async UniTask<GameFlowResult> GoToMenu()
		{
			Debug.Log("GoToMenu");
			var sceneHandler = menuScene.LoadSceneAsync(LoadSceneMode.Single);
			var sceneInstance = await sceneHandler;

			var result = await FindObjectOfType<MenuController>().RunAsync();

			return result;
			//await Addressables.UnloadSceneAsync(sceneInstance);
		}
	}
}

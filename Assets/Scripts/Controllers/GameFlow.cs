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

		public async UniTaskVoid Start()
		{
			GameFlowResult result = new GoToMenuResult();

			do
			{
				switch (result)
				{
					case GoToMenuResult menu:
						result = await GoToMenu();
						break;
				}
				
			} while (result is not QuitResult);
			
		}


		
		private async UniTask<GameFlowResult> GoToMenu()
		{
			var sceneHandler = menuScene.LoadSceneAsync(LoadSceneMode.Single);
			var sceneInstance = await sceneHandler;

			var result = await FindObjectOfType<MenuController>().Run();

			return result;
			//await Addressables.UnloadSceneAsync(sceneInstance);
		}
	}
}

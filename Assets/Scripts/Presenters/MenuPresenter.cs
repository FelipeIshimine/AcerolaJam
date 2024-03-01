using System;
using Controllers.MenuState;
using Cysharp.Threading.Tasks;
using Models.GameFlow;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Presenters
{
	public class MenuPresenter : Presenter<MenuController>
	{
		[SerializeField] private Panel mainPanel;
		//[SerializeField] private Button toggleMusicBtn;
		//[SerializeField] private Button toggleSoundBtn;
		[SerializeField] private Button playBtn;
		[SerializeField] private Button quitBtn;
		
		private void Start()
		{
			Controller.OnCompletionSource += OnRun;
		}

		private async void OnRun(UniTaskCompletionSource<GameFlowResult> completionSource)
		{
			
			Controller.OnCompletionSource -= OnRun;

			mainPanel.Open().Forget();
			
			playBtn.onClick.AddListener(()=> completionSource.TrySetResult(new GoToGameplayResult()));
			quitBtn.onClick.AddListener(()=> completionSource.TrySetResult(new QuitResult()));
			
			await completionSource.Task;
			
			playBtn.onClick.RemoveAllListeners();
			quitBtn.onClick.RemoveAllListeners();

			mainPanel.Close().Forget();
			
		}

	
	}
}

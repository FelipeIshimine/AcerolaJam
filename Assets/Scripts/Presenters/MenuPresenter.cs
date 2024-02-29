using System;
using Controllers.MenuState;
using Cysharp.Threading.Tasks;
using Models.GameFlow;
using UnityEngine;
using Views;

namespace Presenters
{
	public class MenuPresenter : Presenter<MenuController>
	{
		public Panel mainPanel;
		
		private void Start()
		{
			Controller.OnRun += OnRun;
		}

		private async void OnRun(UniTaskCompletionSource<GameFlowResult> completionSource)
		{
			Controller.OnRun -= OnRun;

			mainPanel.Open();
			
			await completionSource.Task;
            
			
		}
	}
}

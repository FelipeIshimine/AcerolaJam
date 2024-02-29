using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models.GameFlow;
using UnityEngine;

namespace Controllers.MenuState
{
	public class MenuController : MonoBehaviour
	{
		public event Action<UniTaskCompletionSource<GameFlowResult>> OnRun; 
		
		public async Task<GameFlowResult> Run()
		{

			UniTaskCompletionSource<GameFlowResult> completionSource = new UniTaskCompletionSource<GameFlowResult>();
			
			OnRun?.Invoke(completionSource);
			var result = await completionSource.Task;
			return result;
		}
		
	}
}
using System;
using Cysharp.Threading.Tasks;
using Models.GameFlow;
using UnityEngine;

namespace Controllers
{
	public abstract class Controller : MonoBehaviour
	{
		public event Action<UniTaskCompletionSource<GameFlowResult>> OnCompletionSource; 
		
		public async UniTask<GameFlowResult> RunAsync()
		{
			UniTaskCompletionSource<GameFlowResult> completionSource = new UniTaskCompletionSource<GameFlowResult>();
			OnRun(completionSource);
			OnCompletionSource?.Invoke(completionSource);
			return await completionSource.Task;
		}

		protected abstract void OnRun(UniTaskCompletionSource<GameFlowResult> completionSource);
	}
}
using System;
using Cysharp.Threading.Tasks;
using Models.GameFlow;

namespace Controllers.GameplayState
{
	public class GameplayController : Controller
	{
		protected override void OnRun(UniTaskCompletionSource<GameFlowResult> completionSource)
		{
		}
	}
}
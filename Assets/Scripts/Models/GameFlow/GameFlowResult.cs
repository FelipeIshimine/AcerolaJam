namespace Models.GameFlow
{
	public abstract class GameFlowResult { }

	public class QuitResult : GameFlowResult { }
	public class GoToMenuResult : GameFlowResult { }
}
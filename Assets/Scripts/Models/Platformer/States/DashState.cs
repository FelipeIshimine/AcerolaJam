using UnityEngine;

namespace Models.Platformer.States
{
	public class DashState : PlatformerMotorState<ICanRunOnGround>
	{
		private readonly Settings settings;
		private float Dir;

		[System.Serializable]
		public record Settings
		{
			[Min(0.01f)]public float Duration = 1;
			public float Velocity = 10;
			public AnimationCurve IntensityCurve = AnimationCurve.Constant(0,1,1);
		}

		public DashState(ICanRunOnGround context, Settings settings) : base(context)
		{
			this.settings = settings;
		}

		protected override void OnEnter() => Dir = Context.MoveDirection;
		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			float t = LifeTime/settings.Duration;
			var velocity = Context.Rb.velocity;

			if (Dir > 0)
			{
				velocity.x = Mathf.Max(velocity.x, settings.IntensityCurve.Evaluate(t)*settings.Velocity);
			}
			else
			{
				velocity.x = Mathf.Min(velocity.x, -settings.IntensityCurve.Evaluate(t)*settings.Velocity);
			}
			
			velocity.y = 0;
			Context.Rb.velocity = velocity;
		}

		protected override PlatformerMotorState OnTransitionCheck() => null;
	}
}
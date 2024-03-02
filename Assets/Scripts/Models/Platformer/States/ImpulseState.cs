using UnityEngine;

namespace Models.Platformer.States
{
	public class ImpulseState : PlatformerMotorState<IGroundedJump>
	{
		private readonly Settings settings;

		[System.Serializable]
		public record Settings
		{
			[Min(0.01f)]public float Duration = 1;
			public float UpwardVelocity = 10;
			public AnimationCurve IntensityCurve = AnimationCurve.Constant(0,1,1);
		}

		public ImpulseState(IGroundedJump context, Settings settings) : base(context)
		{
			this.settings = settings;
		}

		protected override void OnEnter() { }
		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			float t = LifeTime/settings.Duration;
			var velocity = Context.Rb.velocity;
			velocity.y = Mathf.Max(velocity.y, settings.IntensityCurve.Evaluate(t)*settings.UpwardVelocity);
			Context.Rb.velocity = velocity;
			/*if (t >= 1)
			{
				Abort();
			}*/
		}

		protected override PlatformerMotorState OnTransitionCheck() => null;
	}
}
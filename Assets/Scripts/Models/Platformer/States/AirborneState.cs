using UnityEngine;

namespace Models.Platformer.States
{
	public class AirborneState : PlatformerMotorState<ICanRunOnGround>
	{
		[System.Serializable]
		public class Settings
		{
			public float AirMaxSpeed;
			public float AirAcceleration;
		}
		private readonly Settings settings;

		public AirborneState(ICanRunOnGround context, Settings settings) : base(context)
		{
			this.settings = settings;
		}

		protected override void OnEnter() { }
		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			var vel = Context.Rb.velocity;
			var targetSpeed = Mathf.Max(Mathf.Abs(vel.x),
					Mathf.Abs(settings.AirMaxSpeed * Context.MoveDirection)) *
				Mathf.Sign(Context.MoveDirection);
			vel.x = Mathf.MoveTowards(vel.x, targetSpeed, settings.AirAcceleration * Time.deltaTime);
			Context.Rb.velocity = vel;
		}

		protected override PlatformerMotorState OnTransitionCheck() => null;
	}
}
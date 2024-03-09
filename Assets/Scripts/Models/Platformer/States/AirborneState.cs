using UnityEngine;

namespace Models.Platformer.States
{
	public class AirborneState : PlatformerMotorState<ICanRunOnGround>
	{
		private readonly Settings settings;
		private float rotVel;

        
		public AirborneState(ICanRunOnGround context, Settings settings) : base(context)
		{
			this.settings = settings;
		}

		protected override void OnEnter()
		{
			rotVel = 0; }

		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			var vel = Context.Rb.velocity;
			var targetSpeed = Mathf.Max(Mathf.Abs(vel.x),
					Mathf.Abs(settings.AirMaxSpeed * Context.MoveDirection)) *
				Mathf.Sign(Context.MoveDirection);
			vel.x = Mathf.MoveTowards(vel.x, targetSpeed, settings.AirAcceleration * Time.deltaTime);
			Context.Rb.velocity = vel;
			
			Context.Rb.rotation = Mathf.SmoothDampAngle(Context.Rb.rotation,0,ref rotVel, .05f, float.MaxValue);
		}

		protected override PlatformerMotorState OnTransitionCheck() => null;

		[System.Serializable]
		public class Settings
		{
			public float AirMaxSpeed;
			public float AirAcceleration;
		}
	}
}
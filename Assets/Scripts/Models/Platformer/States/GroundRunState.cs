using UnityEngine;

namespace Models.Platformer.States
{
	public class GroundRunState : PlatformerMotorState<IGroundedMovement>
	{
		private readonly Settings settings;

		public GroundRunState(
			IGroundedMovement context,
			Settings settings) : base(context)
		{
			this.settings = settings;
		}

		[System.Serializable]
		public record Settings
		{
			public float GroundMaxSpeed;
			public float GroundAcceleration;
		}


		protected override void OnEnter()
		{
		}

		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			Vector2 moveDir = Vector3.ProjectOnPlane(Vector2.right * Context.MoveDirection,
				Context.GroundNormal);
			
			Context.Rb.velocity = Vector2.MoveTowards(
				Context.Rb.velocity,
				moveDir * settings.GroundMaxSpeed,
				settings.GroundAcceleration * Time.deltaTime);

		}

		protected override PlatformerMotorState OnTransitionCheck() => null;
	}
}
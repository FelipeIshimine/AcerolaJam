using UnityEngine;

namespace Models.Platformer.States
{
	public class GroundIdleState : PlatformerMotorState<IGroundedMovement>
	{
		private Vector2 vel;

		public GroundIdleState(IGroundedMovement context) : base(context)
		{
		}

		protected override void OnEnter()
		{
			vel = Vector2.zero;
		}

		protected override void OnExit()
		{
		}

		protected override void OnFixedUpdate(float delta)
		{
			Context.Rb.velocity = Vector2.SmoothDamp(Context.Rb.velocity, Vector2.zero, ref vel,.2f);
		}

		protected override PlatformerMotorState OnTransitionCheck() => null;
	}
}
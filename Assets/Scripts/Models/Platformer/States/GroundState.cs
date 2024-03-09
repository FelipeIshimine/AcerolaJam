using UnityEngine;

namespace Models.Platformer.States
{
	public class GroundState : PlatformerMotorState<ICanRunOnGround>
	{
		private GroundIdleState groundIdleState;
		private GroundRunState groundRunState;
		private GroundRunState.Settings runStateSettings;
		private float rotVel;

		public GroundState(ICanRunOnGround context, GroundRunState.Settings runStateSettings) : base(context)
		{
			this.runStateSettings = runStateSettings;
		}

		protected override void OnEnter()
		{
		}

		protected override void OnExit()
		{
		}

		protected override void OnFixedUpdate(float delta)
		{
			Context.Rb.rotation = Mathf.SmoothDampAngle(Context.Rb.rotation,-Vector2.Angle(Vector2.up, Context.GroundNormal),ref rotVel, 1f, float.MaxValue);
		}

		protected override PlatformerMotorState OnTransitionCheck()
		{
			if ((!Context.WallContactLeft && Context.MoveDirection < 0) ||
			    (!Context.WallContactRight && Context.MoveDirection > 0))
			{
				return groundRunState ??= new GroundRunState(Context, runStateSettings);
			}
			return groundIdleState ??= new GroundIdleState(Context);
		}
	}
}
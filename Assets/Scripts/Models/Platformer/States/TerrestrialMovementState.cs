using UnityEngine;

namespace Models.Platformer.States
{
	public class TerrestrialMovementState : PlatformerMotorState<ICanRunJumpDash>
	{
		private readonly Settings settings;
		private GroundState groundState;
		private AirborneState airborneState;

		private int remainingAirJump;
		private int remainingCoyoteTime;
		private float UngroundTime = .2f;
		private float remainingUnGroundTime;
		private DashState dashState;

		[System.Serializable]
		public record Settings
		{
			public float UnGroundTimeDuration = .1f;

			public int MaxAirJumps = 2;
			public float jumpVelocity = 10;
			public AirborneState.Settings AirborneStateSettings;
			public GroundRunState.Settings RunningStateSettings;
			public DashState.Settings DashStateSettings;
		}
		
		public TerrestrialMovementState(ICanRunJumpDash context, Settings settings) : base(context)
		{
			this.settings = settings;
		}

		protected override void OnEnter() { }
		protected override void OnExit() { }

		protected override void OnFixedUpdate(float delta)
		{
			remainingUnGroundTime -= Time.deltaTime;
			if (Context.JumpRequested)
			{
				if (Context.IsGrounded || remainingAirJump > 0)
				{
					if (!Context.IsGrounded)
					{
						remainingAirJump--;
					}
					var vel = Context.Rb.velocity;
					vel.y = settings.jumpVelocity;
					Context.Rb.velocity = vel;
					UnGround();
				}
			}
			Context.JumpRequested = false;
		}

		private void UnGround()
		{
			Context.IsGrounded = false;
			remainingCoyoteTime = 0;
			remainingUnGroundTime = UngroundTime;
		}
		
		protected override PlatformerMotorState OnTransitionCheck()
		{
			if (Context.DashRequested || (dashState != null && dashState.LifeTime < settings.DashStateSettings.Duration))
			{
				Context.DashRequested = false;
				return dashState ??= new DashState(Context, settings.DashStateSettings);
			}
			
			if (Context.IsGrounded && remainingUnGroundTime <= 0)
			{
				Debug.Log("B");
				remainingAirJump = settings.MaxAirJumps;
				return groundState ??= new GroundState(Context, settings.RunningStateSettings);
			}

			Debug.Log("C");
			return airborneState ??= new AirborneState(Context, settings.AirborneStateSettings);
		}
	}
}
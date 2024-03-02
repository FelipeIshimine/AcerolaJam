using UnityEngine;

namespace Models.Platformer.States
{
	public class NoneState : PlatformerMotorState
	{
		protected override void OnEnter() { }
		protected override void OnExit() { }
		protected override void OnFixedUpdate(float delta) { }
		protected override PlatformerMotorState OnTransitionCheck() => null;
	}

	

	public interface ICanRunOnGround : IRigidBody2D
	{
		public bool IsGrounded { get; set; }
		public bool WallContactLeft { get; }
		public bool WallContactRight { get; }
		public Vector2 GroundNormal { get; }
		public float MoveDirection { get; }
	}

	public interface ICanJump : IRigidBody2D
	{
		public bool JumpRequested {get; set;}
	}
	public interface ICanDash : IRigidBody2D
	{
		public bool DashRequested {get; set;}
	}

	public interface IRigidBody2D
	{
		public Rigidbody2D Rb { get; set; }
	}

	public interface ICanRunJumpDash : ICanRunOnGround, ICanJump,ICanDash
	{
	}
}


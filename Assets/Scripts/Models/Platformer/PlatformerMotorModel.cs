using UnityEngine;

namespace Models.Platformer
{
	[System.Serializable]
	public struct PlatformerMotorModel 
	{
		public bool IsGrounded;
		public bool IsAirborne => !IsGrounded;

		public bool WallContactLeft;
		public bool WallContactRight;
		public Vector2 GroundNormal;
		public float MoveDirection;


		//Triggers
		public bool Dash;
		public bool Jump;
	}
}
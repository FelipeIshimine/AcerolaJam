using Models.Platformer.States;
using UnityEngine;

namespace Models.Platformer
{
	[System.Serializable]
	public class PlayerMovementStatus : MonoBehaviour, ICanRunJumpDash
	{
		public Rigidbody2D Rb { get; set; }
		[field:SerializeField] public bool IsGrounded { get; set; }
		public bool IsAirborne => !IsGrounded;
		
		[field:SerializeField] public bool WallContactLeft {get; set;}
		[field:SerializeField] public bool WallContactRight {get; set;}
		[field:SerializeField] public Vector2 GroundNormal {get; set;}
		[field:SerializeField] public float MoveDirection {get; set;}

		//Triggers
		[field:SerializeField] public bool JumpRequested {get; set;}
		[field:SerializeField] public bool DashRequested { get; set; }
	}
}
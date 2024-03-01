using UnityEngine;

namespace Controllers
{
	[System.Serializable]
	public record PlatformerMotorSettings
	{
		[SerializeField] public float JumpVelocity = 6;
		[SerializeField] public float GroundMaxSpeed = 5;
		[SerializeField] public float GroundAcceleration = 30;
		[SerializeField] public float AirMaxSpeed = 5;
		[SerializeField] public float AirAcceleration = 5;
		[SerializeField] public int MaxAirJumps = 3;
		
		[SerializeField] public int DashCount = 1;
		[SerializeField] public float DashMaxSpeed = 3;
		[SerializeField] public float DashCooldown = .2f;
		
		[SerializeField] public float CoyoteTime = .025f;
		[SerializeField] public float UngroundTime = .1f;
	}
}
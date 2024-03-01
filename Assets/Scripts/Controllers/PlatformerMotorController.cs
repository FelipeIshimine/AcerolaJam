using Models.Platformer;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlatformerMotorController : MonoBehaviour, InputMap.IGameplayActions
	{
		private Rigidbody2D rb;
		private InputMap inputMap;

		[SerializeField] private ContactFilter2D groundFilter;
		[SerializeField] private BoxCollider2D boxCollider;
		[SerializeField] private PlatformerMotorModel model;

		[SerializeField] private float groundSensibility = .6f;
		[SerializeField] private float wallSensibility = .5f;

		[SerializeField] private PlatformerMotorSettings settings = new PlatformerMotorSettings();
	
		private static ContactPoint2D[] contacts = new ContactPoint2D[128];

		[ReadOnly,ShowNonSerializedField] private float remainingCoyoteTime;
		[ReadOnly,ShowNonSerializedField] private float remainingUnGroundTime;
		[ReadOnly,ShowNonSerializedField] private int remainingAirJumps;
		[ReadOnly,ShowNonSerializedField] private int remainingDashes;
		[ReadOnly,ShowNonSerializedField] private float remainingDashCooldown;
		
		[Button]
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			inputMap = new InputMap();
			inputMap.Gameplay.AddCallbacks(this);
		}

		private void OnEnable()
		{
			inputMap.Gameplay.Enable();
			inputMap.Enable();
		}

		private void OnDisable()
		{
			inputMap.Gameplay.Disable();
			inputMap.Disable();
		}

		#region Inputs

		public void OnJump(InputAction.CallbackContext context)
		{
			Debug.Log($"OnJump:{context.performed}");
			if (context.performed)
			{
				model.Jump = true;
			}
		}

		public void OnHorizontalMovement(InputAction.CallbackContext context)
		{
			model.MoveDirection = context.ReadValue<float>();
		}

		public void OnDash(InputAction.CallbackContext context)
		{
			if (context.performed && remainingDashes > 0)
			{
				model.Dash = true;
			}
		}	
		#endregion

		private void FixedUpdate()
		{
			ProcessState();

			var vel = rb.velocity;
			
			if (model.Jump && CanJump())
			{
				if (!model.IsGrounded)
				{
					remainingAirJumps--;
				}
				
				vel.y = Mathf.Max(vel.y, settings.JumpVelocity);

				/*if (model.IsGrounded)
				{
					vel.y += Mathf.Max(CalculateGroundSpeed(rb.velocity).y,0);
				}*/
				
				Unground();
			}
			model.Jump = false;

			if (model.IsGrounded)
			{
				remainingDashes = settings.DashCount;
				remainingDashCooldown = 0;
			}
			
			if (model.Dash && remainingDashes > 0)
			{
				remainingDashes--;
				model.Dash = false;
				
				vel.x = Mathf.Max(vel.y, settings.DashMaxSpeed);
				vel.y = 0;
				
				remainingUnGroundTime = settings.DashCooldown;
				Unground();
			}
			
			model.Dash = false;
			
			if ((!model.WallContactLeft && model.MoveDirection < 0) ||
			    (!model.WallContactRight && model.MoveDirection > 0))
			{
				if (model.IsGrounded && remainingUnGroundTime <= 0)
				{
					if (model.MoveDirection != 0)
					{
						vel = CalculateGroundSpeed(vel);
					}
					else
					{
						vel = Vector2.zero;
					}
				}
				else
				{
					var targetSpeed = Mathf.Max(Mathf.Abs(vel.x),
							Mathf.Abs(settings.AirMaxSpeed * model.MoveDirection)) *
						Mathf.Sign(model.MoveDirection);
					vel.x = Mathf.MoveTowards(vel.x, targetSpeed, settings.AirAcceleration * Time.deltaTime);
				}
			}

			rb.velocity = vel;
		}

		private void Unground()
		{
			model.IsGrounded = false;
			remainingCoyoteTime = 0;
			remainingUnGroundTime = settings.UngroundTime;
		}

		private Vector2 CalculateGroundSpeed(Vector2 vel)
		{
			Vector2 moveDir = Vector3.ProjectOnPlane(Vector2.right * model.MoveDirection,
				model.GroundNormal);
			var groundSpeed = Vector2.MoveTowards(vel, moveDir * settings.GroundMaxSpeed,
				settings.GroundAcceleration * Time.deltaTime);
			return groundSpeed;
		}

		private bool CanJump() => (model.IsGrounded || remainingAirJumps > 0);

		private void ProcessState()
		{
			int count = rb.GetContacts(contacts);
			bool grounded = false;
		
			//Floor Check
			for (int i = 0; i < count; i++)
			{
				var dot = Vector2.Dot(contacts[i].normal, Vector2.up);
				if (groundSensibility <= dot)
				{
					grounded = true;
					model.GroundNormal = contacts[i].normal;
					remainingCoyoteTime = settings.CoyoteTime;
					remainingAirJumps = settings.MaxAirJumps;
					break;
				}
			}

			//Left Wall Check
			model.WallContactLeft = CheckAnyContactAngle(count, Vector2.right, wallSensibility);
		
			//Right Wall Check
			model.WallContactRight = CheckAnyContactAngle(count, Vector2.left, wallSensibility);

			if (grounded || remainingCoyoteTime > 0)
			{
				remainingCoyoteTime -= Time.fixedDeltaTime;
				grounded = true;
			}
			
			remainingUnGroundTime -= Time.fixedDeltaTime;

			remainingDashCooldown -= Time.fixedDeltaTime;
			
			
			
				
			model.Jump &= CanJump();

			model.IsGrounded = grounded;

		}

		private bool CheckAnyContactAngle(int count, Vector2 dir, float sensibility)
		{
			for (int i = 0; i < count; i++)
			{
				var dot = Vector2.Dot(contacts[i].normal, dir);
				if (dot >= sensibility)
				{
					return true;
				}
			}
			return false;
		}
	}
}
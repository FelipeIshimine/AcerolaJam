using System;
using System.Collections.Generic;
using Models.Platformer;
using Models.Platformer.States;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
	[RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovementStatus))]
	public class PlayerMotorController : MonoBehaviour, InputMap.IGameplayActions
	{
		private Rigidbody2D rb;
		private InputMap inputMap;

		[SerializeField] private ContactFilter2D groundFilter;
		private PlayerMovementStatus status;

		[SerializeField] private float groundSensibility = .6f;
		[SerializeField] private float wallSensibility = .5f;

		[SerializeField] private PlatformerMotorSettings settings = new PlatformerMotorSettings();
	
		private static ContactPoint2D[] contacts = new ContactPoint2D[128];

		[ReadOnly,ShowNonSerializedField] private float remainingCoyoteTime;
		[ReadOnly,ShowNonSerializedField] private float remainingUnGroundTime;
		[ReadOnly,ShowNonSerializedField] private int remainingAirJumps;
		[ReadOnly,ShowNonSerializedField] private int remainingDashes;
		[ReadOnly,ShowNonSerializedField] private float remainingDashCooldown;

		[SerializeField] private TerrestrialMovementState.Settings movementSettings;
		[SerializeReference] private PlatformerMotorState movementState = new NoneState();

		[Button]
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			status = GetComponent<PlayerMovementStatus>();
			inputMap = new InputMap();
			inputMap.Gameplay.AddCallbacks(this);

			status.Rb = rb;
			movementState = new TerrestrialMovementState(status, movementSettings);
			movementState.Enter();
		}

		private void OnDestroy()
		{
			movementState.Exit();
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
				this.status.JumpRequested = true;
			}
		}

		public void OnHorizontalMovement(InputAction.CallbackContext context)
		{
			this.status.MoveDirection = context.ReadValue<float>();
		}

		public void OnDash(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				status.DashRequested = true;
			}
		}	
		#endregion

		private void FixedUpdate()
		{
			ProcessState();

			movementState.FixedUpdate(Time.fixedDeltaTime);
			
			return;
			var vel = rb.velocity;
			
			if (status.JumpRequested && CanJump())
			{
				if (!status.IsGrounded)
				{
					remainingAirJumps--;
				}
				
				vel.y = Mathf.Max(vel.y, settings.JumpVelocity);

				Unground();
			}
			status.JumpRequested = false;

			if (status.IsGrounded)
			{
				remainingDashes = settings.DashCount;
				remainingDashCooldown = 0;
			}
			
			if (status.DashRequested && remainingDashes > 0)
			{
				remainingDashes--;
				status.DashRequested = false;
				
				vel.x = Mathf.Max(vel.y, settings.DashMaxSpeed);
				vel.y = 0;
				
				remainingUnGroundTime = settings.DashCooldown;
				Unground();
			}
			
			status.DashRequested = false;
			
			if ((!status.WallContactLeft && status.MoveDirection < 0) ||
			    (!status.WallContactRight && status.MoveDirection > 0))
			{
				if (status.IsGrounded && remainingUnGroundTime <= 0)
				{
					if (status.MoveDirection != 0)
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
							Mathf.Abs(settings.AirMaxSpeed * status.MoveDirection)) *
						Mathf.Sign(status.MoveDirection);
					vel.x = Mathf.MoveTowards(vel.x, targetSpeed, settings.AirAcceleration * Time.deltaTime);
				}
			}

			rb.velocity = vel;
		}

		private void Unground()
		{
			status.IsGrounded = false;
			remainingCoyoteTime = 0;
			remainingUnGroundTime = settings.UngroundTime;
		}

		private Vector2 CalculateGroundSpeed(Vector2 vel)
		{
			Vector2 moveDir = Vector3.ProjectOnPlane(Vector2.right * status.MoveDirection,
				status.GroundNormal);
			var groundSpeed = Vector2.MoveTowards(vel, moveDir * settings.GroundMaxSpeed,
				settings.GroundAcceleration * Time.deltaTime);
			return groundSpeed;
		}

		private bool CanJump() => (status.IsGrounded || remainingAirJumps > 0);

		private void ProcessState()
		{
			int count = rb.GetContacts(contacts);
			bool grounded = false;
		
			//Floor Check
			List<ContactPoint2D> floorContacts = new List<ContactPoint2D>();  
			for (int i = 0; i < count; i++)
			{
				var dot = Vector2.Dot(contacts[i].normal, Vector2.up);
				if (groundSensibility <= dot)
				{
					floorContacts.Add(contacts[i]);
					break;
				}
			}

			if (floorContacts.Count > 0)
			{
				grounded = true;
				remainingCoyoteTime = settings.CoyoteTime;
				remainingAirJumps = settings.MaxAirJumps;

				status.GroundNormal = Vector2.zero;

				for (int i = 0; i < floorContacts.Count; i++)
				{
					status.GroundNormal += contacts[i].normal;
					Debug.DrawRay(contacts[i].point, contacts[i].normal, Color.yellow);
				}


				status.GroundNormal.Normalize();
				Debug.DrawRay(transform.position, status.GroundNormal, Color.magenta);
			}
			

			//Left Wall Check
			status.WallContactLeft = CheckAnyContactAngle(count, Vector2.right, wallSensibility);
		
			//Right Wall Check
			status.WallContactRight = CheckAnyContactAngle(count, Vector2.left, wallSensibility);

			if (grounded || remainingCoyoteTime > 0)
			{
				remainingCoyoteTime -= Time.fixedDeltaTime;
				grounded = true;
			}
			
			remainingUnGroundTime -= Time.fixedDeltaTime;

			remainingDashCooldown -= Time.fixedDeltaTime;
			
				
			status.JumpRequested &= CanJump();

			status.IsGrounded = grounded;

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
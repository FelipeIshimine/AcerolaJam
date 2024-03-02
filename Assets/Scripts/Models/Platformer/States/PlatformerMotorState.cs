using System;
using NaughtyAttributes;
using UnityEngine;

namespace Models.Platformer.States
{
	[System.Serializable]
	public abstract class PlatformerMotorState
	{
		public event Action OnAbort;
		[field:SerializeReference] public PlatformerMotorState SubState { get; private set; }
		
		public bool IsLive { get; private set; }
		protected internal float LifeTime { get; private set; }
		public void Enter()
		{
			IsLive = true;
			LifeTime = 0;
			Debug.Log($"{this}.Enter");
			SubState?.Enter();
			OnEnter();
			TransitionCheck();
		}

		protected abstract void OnEnter();
		public void Exit()
		{
			IsLive = false;
			Debug.Log($"{this}.Exit");
			SubState?.Exit();
			OnExit();
		}
		protected  abstract void OnExit();
		
		public void FixedUpdate(float delta)
		{
			LifeTime += delta;
			TransitionCheck();
			Debug.Log($"{GetType().Name}.FixedUpdate {LifeTime}");
			SubState?.FixedUpdate(delta);
			OnFixedUpdate(delta);

		}
		protected abstract void OnFixedUpdate(float delta);
		
		public void TransitionCheck()
		{
			var result = OnTransitionCheck();
			//Debug.Log($"{this}.TransitionCheck:{result}");
			Transition(result);
		}
		
		protected abstract PlatformerMotorState OnTransitionCheck();

		private void Transition(PlatformerMotorState state)
		{
			if (state == SubState)
			{
				return;
			}
			Debug.Log($"[{GetType().Name}] {SubState}=>{state}");

			if (SubState != null)
			{
				SubState.OnAbort -= AbortSubState;
				SubState.Exit();
			}
			SubState = state;
			if (SubState != null)
			{
				SubState.OnAbort += AbortSubState;
				SubState.Enter();
			}
		}

		private void AbortSubState() => Transition(null);
		protected void Abort() => OnAbort?.Invoke();
	}

	public abstract class PlatformerMotorState<T> : PlatformerMotorState
	{
		protected readonly T Context;
		protected PlatformerMotorState(T context)
		{
			Context = context;
		}
	} 
}


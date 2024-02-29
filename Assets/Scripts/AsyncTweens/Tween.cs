using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Tween
{
	#region Delegates
	public Action<Tween> onUpdate;
	public Action<Tween> onStateChange;
	public Action<Tween> onComplete;
	public Action<Tween> onLoop;
	#endregion

	#region Enums
	public enum TimeType
	{
		Normal,
		Real,
		Fixed,
	};

	public enum PlayState
	{
		Stopped,
		Paused,
		Playing
	};

	public enum Direction
	{
		Forward,
		Reverse
	};

	public enum EndBehaviour
	{
		Constant,
		Reset,
	};

	public enum LoopType
	{
		Repeat,
		PingPong,
	};
	#endregion

	#region Fields & Properties
	public static float DefaultDuration = 1f;
	public static Func<float, float, float, float> DefaultEquation = EasingEquations.EaseInOutQuad;

	public TimeType timeType = TimeType.Normal;
	public PlayState playState { get; private set; }
	public Direction direction { get; private set; }
	public PlayState previousPlayState { get; private set; }
	public EndBehaviour endBehaviour = EndBehaviour.Constant;
	public LoopType loopType = LoopType.Repeat;

	public float startValue = 0.0f;
	public float endValue = 1.0f;
	public float duration = 1.0f;
	public int loopCount = 0;
	public Func<float, float, float, float> equation = EasingEquations.Linear;

	public float currentTime { get; private set; }
	public float currentValue { get; private set; }
	public float currentOffset { get; private set; }
	public int loops { get; private set; }

	private static CancellationTokenSource cts = new CancellationTokenSource();
	#endregion

	#region
	/*public async UniTask Play()
    {
	    //Debug.Log($"PLAY{this}");
		await Play(cts.Token);
    }*/

	public async UniTask Play(CancellationToken cancellationToken)
	{
		playState = PlayState.Playing;
		while (playState != PlayState.Stopped)
		{
			var time = await Tick(cancellationToken);
			var finished = Progress(time);
			OnUpdate();
			if (finished)
			{
				++loops;
				if (loopCount < 0 || loopCount >= loops)
				{
					if (loopType == LoopType.Repeat)
						SeekToTime(direction == Direction.Forward ? 0.0f : duration);
					else // PingPong
						SetDirection(direction == Direction.Forward ? Direction.Reverse : Direction.Forward);

					OnLoop();
				}
				else
				{
					OnComplete();
					Stop();
				}
			}
		}
	}

	public void Pause()
	{
		if (playState == PlayState.Playing)
			SetPlayState(PlayState.Paused);
	}

	public void Resume()
	{
		if (playState == PlayState.Paused)
			SetPlayState(PlayState.Playing);
	}

	public void Stop()
	{
		SetPlayState(PlayState.Stopped);
		loops = 0;
		if (endBehaviour == EndBehaviour.Reset)
			SeekToBeginning();
	}

	public void SetDirection(Direction value)
	{
		direction = value;
	}

	public void SetPlayState(PlayState value)
	{
		playState = value;
	}

	public void SeekToTime(float time)
	{
		currentTime = Mathf.Clamp01(time / duration);
		float newValue = (endValue - startValue) * currentTime + startValue;
		currentOffset = newValue - currentValue;
		currentValue = newValue;
		OnUpdate();
	}

	public void SeekToBeginning()
	{
		SeekToTime(0.0f);
	}

	public void SeekToEnd()
	{
		SeekToTime(duration);
	}
	
	public Tween SetTime(TimeType timeType)
	{
		this.timeType = timeType;
		return this;
	}
	#endregion

	#region Protected
	protected virtual void OnUpdate()
	{
		if (onUpdate != null)
			onUpdate(this);
	}

	protected virtual void OnLoop()
	{
		if (onLoop != null)
			onLoop(this);
	}

	protected virtual void OnComplete()
	{
		if (onComplete != null)
			onComplete(this);
	}

	protected virtual void OnStateChange()
	{
		if (onStateChange != null)
			onStateChange(this);
	}
	#endregion

	#region Private
	async UniTask<float> Tick(CancellationToken cancellationToken)
	{
		float frameTime = 0;
		switch (timeType)
		{
			case TimeType.Normal:
				await UniTask.NextFrame(cancellationToken);
				frameTime = Time.deltaTime;
				break;
			case TimeType.Real:
				await UniTask.NextFrame(cancellationToken);
				frameTime = Time.unscaledDeltaTime;
				break;
			case TimeType.Fixed:
				await UniTask.WaitForFixedUpdate(cancellationToken);
				frameTime = Time.fixedDeltaTime;
				break;
		}
		return frameTime;
	}

	bool Progress(float time)
	{
		bool finished = false;
		if (direction == Direction.Forward)
		{
			currentTime = Mathf.Clamp01(currentTime + (time / duration));
			finished = Mathf.Approximately(currentTime, 1.0f);
		}
		else // Direction.Referse
		{
			currentTime = Mathf.Clamp01(currentTime - (time / duration));
			finished = Mathf.Approximately(currentTime, 0.0f);
		}
		float frameValue = (endValue - startValue) * equation(0.0f, 1.0f, currentTime) + startValue;
		currentOffset = frameValue - currentValue;
		currentValue = frameValue;
		return finished;
	}
	#endregion
	
	
}
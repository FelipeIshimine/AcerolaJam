using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionTween : Tween
{
	public Quaternion startTweenValue;
	public Quaternion endTweenValue;
	public Quaternion currentTweenValue { get; private set; }

	protected override void OnUpdate()
	{
		currentTweenValue = Quaternion.SlerpUnclamped(startTweenValue, endTweenValue, currentValue);
		base.OnUpdate();
	}
}
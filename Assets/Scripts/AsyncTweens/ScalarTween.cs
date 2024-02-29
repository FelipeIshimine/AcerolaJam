using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarTween : Tween
{
	public float startTweenValue;
	public float endTweenValue;
	public float currentTweenValue { get; private set; }

	protected override void OnUpdate()
	{
		currentTweenValue = (endTweenValue - startTweenValue) * currentValue + startTweenValue;
		base.OnUpdate();
	}
}
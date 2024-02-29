using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupAlphaTween : ScalarTween
{
	public CanvasGroup target;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		target.alpha = currentTweenValue;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CanvasGroupAnimationExtensions
{
	public static CanvasGroupAlphaTween FadeOut(this CanvasGroup canvasGroup)
	{
		return canvasGroup.FadeOut(Tween.DefaultDuration, Tween.DefaultEquation);
	}

	public static CanvasGroupAlphaTween FadeOut(this CanvasGroup canvasGroup, float duration)
	{
		return canvasGroup.FadeOut(duration, Tween.DefaultEquation);
	}

	
	public static CanvasGroupAlphaTween FadeOut(this CanvasGroup canvasGroup, float duration, AnimationCurve curve) => canvasGroup.AlphaTween(canvasGroup.alpha, 0, duration, (_,_,time) => curve.Evaluate(time));

	public static CanvasGroupAlphaTween FadeOut(this CanvasGroup canvasGroup, float duration, Func<float, float, float, float> equation)
    {
		return canvasGroup.AlphaTween(canvasGroup.alpha, 0, duration, equation);
    }

	public static CanvasGroupAlphaTween FadeIn(this CanvasGroup canvasGroup)
    {
		return canvasGroup.FadeIn(Tween.DefaultDuration, Tween.DefaultEquation);
    }

	public static CanvasGroupAlphaTween FadeIn(this CanvasGroup canvasGroup, float duration)
    {
		return canvasGroup.FadeIn(duration, Tween.DefaultEquation);
    }

	public static CanvasGroupAlphaTween FadeIn(this CanvasGroup canvasgroup,
	                                           float duration,
	                                           AnimationCurve openAlphaCurve) => FadeIn(canvasgroup, duration,
		(_, _, time) => openAlphaCurve.Evaluate(time));

	public static CanvasGroupAlphaTween FadeIn(this CanvasGroup canvasGroup, float duration, Func<float, float, float, float> equation)
	{
		return canvasGroup.AlphaTween(canvasGroup.alpha, 1, duration, equation);
	}

	public static CanvasGroupAlphaTween AlphaTween(this CanvasGroup canvasGroup, float fromValue, float toValue, float duration, Func<float, float, float, float> equation)
    {
		var result = new CanvasGroupAlphaTween();
		result.target = canvasGroup;
		result.startTweenValue = fromValue;
		result.endTweenValue = toValue;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	
}

using System;
using UnityEngine;

public static class SpriteRendererAnimationExtensions
{
	public static SpriteRendererColorTween ColorTo(this SpriteRenderer t, Color position)
	{
		return ColorTo(t, position, Tween.DefaultDuration);
	}

	public static SpriteRendererColorTween ColorTo(this SpriteRenderer t, Color position, float duration)
	{
		return ColorTo(t, position, duration, Tween.DefaultEquation);
	}

	public static SpriteRendererColorTween ColorTo(this SpriteRenderer t,
	                                               Color targetColor,
	                                               float duration,
	                                               AnimationCurve curve) =>
		                                               ColorTo(t, targetColor, duration, (_, _, time) => curve.Evaluate(time));

	public static SpriteRendererColorTween ColorTo(this SpriteRenderer t,
	                                               Color targetColor,
	                                               float duration,
	                                               Func<float, float, float, float> equation)
	{
		var result = new SpriteRendererColorTween();
		result.spriteRenderer = t;
		result.startTweenValue = t.color;
		result.endTweenValue = targetColor;
		result.duration = duration;
		result.equation = equation;
		return result;
	}
}
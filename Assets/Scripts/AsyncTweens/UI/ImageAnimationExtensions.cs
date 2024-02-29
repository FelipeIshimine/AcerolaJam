using System;
using UnityEngine;
using UnityEngine.UI;

public static class ImageAnimationExtensions
{
	public static ImageColorTween ColorTo(this Image t, Color position)
	{
		return ColorTo(t, position, Tween.DefaultDuration);
	}

	public static ImageColorTween ColorTo(this Image t, Color position, float duration)
	{
		return ColorTo(t, position, duration, Tween.DefaultEquation);
	}

	public static ImageColorTween ColorTo(this Image t, Color targetColor, float duration, AnimationCurve curve) =>
		ColorTo(t, targetColor, duration, (_, _, time) => curve.Evaluate(time));
	
	public static ImageColorTween ColorTo(this Image t, Color targetColor, float duration, Func<float, float, float, float> equation)
	{
		var result = new ImageColorTween();
		result.image = t;
		result.startTweenValue = t.color;
		result.endTweenValue = targetColor;
		result.duration = duration;
		result.equation = equation;
		return result;
	}
}
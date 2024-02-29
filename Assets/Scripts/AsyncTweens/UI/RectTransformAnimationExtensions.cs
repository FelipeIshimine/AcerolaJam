using UnityEngine;
using System;
using System.Collections;

public static class RectTransformAnimationExtensions
{
	public static RectTransformAnchorPositionTween AnchorTo(this RectTransform t, Vector3 position)
	{
		return AnchorTo(t, position, Tween.DefaultDuration);
	}

	public static RectTransformAnchorPositionTween AnchorTo(this RectTransform t, Vector3 position, float duration)
	{
		return AnchorTo(t, position, duration, Tween.DefaultEquation);
	}

	public static RectTransformAnchorPositionTween AnchorTo(this RectTransform t, Vector3 position, float duration, AnimationCurve curve) => AnchorTo(t, position, duration, (_, _, time) => curve.Evaluate(time));

	public static RectTransformAnchorPositionTween AnchorTo(this RectTransform t, Vector3 position, float duration, Func<float, float, float, float> equation)
	{
		var result = new RectTransformAnchorPositionTween();
		result.rectTransform = t;
		result.startTweenValue = t.anchoredPosition;
		result.endTweenValue = position;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	public static LayoutTween Layout(this RectTransform t, Layout fromLayout, Layout toLayout)
    {
		return Layout(t, fromLayout, toLayout, Tween.DefaultDuration);
	}

	public static LayoutTween Layout(this RectTransform t, Layout fromLayout, Layout toLayout, float duration)
    {
		return Layout(t, fromLayout, toLayout, duration, Tween.DefaultEquation);
    }

	public static LayoutTween Layout(this RectTransform t, Layout fromLayout, Layout toLayout, float duration, Func<float, float, float, float> equation)
    {
		var result = new LayoutTween();
		result.rectTransform = t;
		result.startLayout = fromLayout;
		result.endLayout = toLayout;
		result.duration = duration;
		result.equation = equation;
		return result;
    }
}
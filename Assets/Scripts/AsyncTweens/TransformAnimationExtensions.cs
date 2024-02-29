using UnityEngine;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;

public static class TransformAnimationExtensions
{
	public static TransformPositionTween MoveTo(this Transform t, Vector3 position)
	{
		return MoveTo(t, position, Tween.DefaultDuration);
	}

	public static TransformPositionTween MoveTo(this Transform t, Vector3 position, float duration)
	{
		return MoveTo(t, position, duration, Tween.DefaultEquation);
	}

	public static TransformPositionTween MoveTo(this Transform t, Vector3 position, float duration, Func<float, float, float, float> equation)
	{
		var result = new TransformPositionTween();
		result.transform = t;
		result.startTweenValue = t.position;
		result.endTweenValue = position;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	public static TransformPositionTween MoveTo(this Transform t, Vector3 position, float duration, AnimationCurve curve) =>
		MoveTo(t, position, duration, (_, _, time) => curve.Evaluate(time));
	
	public static TransformLocalPositionTween MoveToLocal(this Transform t, Vector3 position)
	{
		return MoveToLocal(t, position, Tween.DefaultDuration);
	}

	public static TransformLocalPositionTween MoveToLocal(this Transform t, Vector3 position, float duration)
	{
		return MoveToLocal(t, position, duration, Tween.DefaultEquation);
	}

	public static TransformLocalPositionTween MoveToLocal(this Transform t, Vector3 position, float duration, AnimationCurve curve) =>
		MoveToLocal(t, position, duration, (_, _, time) => curve.Evaluate(time));
	
	public static TransformLocalPositionTween MoveToLocal(this Transform t, Vector3 position, float duration, Func<float, float, float, float> equation)
	{
		var result = new TransformLocalPositionTween();
		result.transform = t;
		result.startTweenValue = t.localPosition;
		result.endTweenValue = position;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	public static TransformRotationTween RotateTo(this Transform t, Quaternion rotation)
	{
		return RotateTo(t, rotation, Tween.DefaultDuration);
	}

	public static TransformRotationTween RotateTo(this Transform t, Quaternion rotation, float duration)
	{
		return RotateTo(t, rotation, duration, Tween.DefaultEquation);
	}

	public static TransformRotationTween RotateTo(this Transform t, Quaternion rotation, float duration, AnimationCurve curve) =>
		RotateTo(t, rotation, duration, (_, _, time) => curve.Evaluate(time));

	
	public static TransformRotationTween RotateTo(this Transform t, Quaternion rotation, float duration, Func<float, float, float, float> equation)
	{
		var result = new TransformRotationTween();
		result.transform = t;
		result.startTweenValue = t.rotation;
		result.endTweenValue = rotation;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	public static TransformLocalEulerTween RotateToLocal(this Transform t, Vector3 euler, float duration, AnimationCurve curve) =>
		RotateToLocal(t, euler, duration, (_, _, time) => curve.Evaluate(time));

	
	public static TransformLocalEulerTween RotateToLocal(this Transform t, Vector3 euler)
	{
		return RotateToLocal(t, euler, Tween.DefaultDuration);
	}

	public static TransformLocalEulerTween RotateToLocal(this Transform t, Vector3 euler, float duration)
	{
		return RotateToLocal(t, euler, duration, Tween.DefaultEquation);
	}

	public static TransformLocalEulerTween RotateToLocal(this Transform t, Vector3 euler, float duration, Func<float, float, float, float> equation)
	{
		var result = new TransformLocalEulerTween();
		result.transform = t;
		result.startTweenValue = t.localEulerAngles;
		result.endTweenValue = euler;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

	public static TransformScaleTween ScaleTo(this Transform t, Vector3 scale)
	{
		return ScaleTo(t, scale, Tween.DefaultDuration);
	}

	public static TransformScaleTween ScaleTo(this Transform t, Vector3 scale, float duration)
	{
		return ScaleTo(t, scale, duration, Tween.DefaultEquation);
	}

	public static TransformScaleTween ScaleTo(this Transform t, Vector3 scale, float duration, AnimationCurve curve) =>
		ScaleTo(t, scale, duration, (_, _, time) => curve.Evaluate(time));

	
	public static TransformScaleTween ScaleTo(this Transform t, Vector3 scale, float duration, Func<float, float, float, float> equation)
	{
		var result = new TransformScaleTween();
		result.transform = t;
		result.startTweenValue = t.localScale;
		result.endTweenValue = scale;
		result.duration = duration;
		result.equation = equation;
		return result;
	}

}



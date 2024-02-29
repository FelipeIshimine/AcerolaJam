using UnityEngine;
using System.Collections;

public class TransformScaleTween : Vector3Tween
{
	public Transform transform;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		transform.localScale = currentTweenValue;
	}

}
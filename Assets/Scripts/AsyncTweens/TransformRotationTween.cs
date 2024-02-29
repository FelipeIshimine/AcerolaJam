using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotationTween : QuaternionTween
{
	public Transform transform;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		transform.rotation = currentTweenValue;
	}
}
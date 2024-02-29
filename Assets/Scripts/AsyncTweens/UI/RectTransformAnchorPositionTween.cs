using UnityEngine;
using System.Collections;

public class RectTransformAnchorPositionTween : Vector3Tween
{
	public RectTransform rectTransform;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		rectTransform.anchoredPosition = currentTweenValue;
	}
}

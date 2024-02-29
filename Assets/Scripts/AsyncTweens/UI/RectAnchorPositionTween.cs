using UnityEngine;

public class RectAnchorPositionTween : Vector3Tween
{
	public RectTransform rectTransform;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		rectTransform.anchoredPosition = currentTweenValue;
	}
}
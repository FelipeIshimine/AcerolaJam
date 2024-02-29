using UnityEngine;

public class ColorTween : Tween
{
	public Color startTweenValue;
	public Color endTweenValue;
	public Color currentTweenValue { get; private set; }

	protected override void OnUpdate()
	{
		currentTweenValue = Color.Lerp(startTweenValue,endTweenValue,currentValue);
		base.OnUpdate();
	}
}
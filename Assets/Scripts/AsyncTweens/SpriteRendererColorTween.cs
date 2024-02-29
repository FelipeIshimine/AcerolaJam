using UnityEngine;

public class SpriteRendererColorTween : ColorTween
{
	public SpriteRenderer spriteRenderer;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		spriteRenderer.color = currentTweenValue;
	}
}
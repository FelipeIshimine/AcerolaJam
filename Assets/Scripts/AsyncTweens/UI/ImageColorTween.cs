using UnityEngine.UI;

public class ImageColorTween : ColorTween
{
	public Image image;

	protected override void OnUpdate()
	{
		base.OnUpdate();
		image.color = currentTweenValue;
	}
}
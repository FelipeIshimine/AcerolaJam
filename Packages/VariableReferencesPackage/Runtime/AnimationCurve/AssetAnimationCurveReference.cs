using UnityEngine;

[System.Serializable]
public class AssetAnimationCurveReference : AnimationCurveReference
{
	[SerializeField] private AnimationCurveAsset target;
    protected override AnimationCurve GetValue() => target.Value;
    protected override void SetValue(AnimationCurve newValue) => target.Value = newValue;
}

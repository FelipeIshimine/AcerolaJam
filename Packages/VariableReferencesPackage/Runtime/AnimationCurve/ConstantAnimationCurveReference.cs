using UnityEngine;

[System.Serializable]
public class ConstantAnimationCurveReference : AnimationCurveReference 
{
	[SerializeField] private AnimationCurve target;
    protected override AnimationCurve GetValue() => target;
    protected override void SetValue(AnimationCurve newValue) => target = newValue;
}

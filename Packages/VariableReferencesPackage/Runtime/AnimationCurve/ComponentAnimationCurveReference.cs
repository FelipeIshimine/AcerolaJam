using UnityEngine;

[System.Serializable]
public class ComponentAnimationCurveReference : AnimationCurveReference 
{
	[SerializeField] private AnimationCurveComponent target;
    
    protected override AnimationCurve GetValue() => target.Value;
    protected override void SetValue(AnimationCurve newValue) => target.Value = newValue;
        
}

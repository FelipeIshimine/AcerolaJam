using UnityEngine;
using VariableReferences;

[System.Serializable]
public class ProceduralAnimationCurve : ProceduralVariable<AnimationCurve, AnimationCurveReference, AnimationCurveDecorator>
{
    public ProceduralAnimationCurve()
	{
		baseValue = new ConstantAnimationCurveReference();
	}
}

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace Views
{
	[RequireComponent(typeof(CanvasGroup))]
	public class Panel : MonoBehaviour
	{
		private RectTransform RT => (RectTransform)transform;

		private CanvasGroup canvasGroup;
		protected CanvasGroup CanvasGroup
		{
			get
			{
				if (canvasGroup == null)
				{
					canvasGroup = GetComponent<CanvasGroup>();
				}
				return canvasGroup;
			}
		}

		[SerializeField] private float duration = .25f; 
		
		[SerializeField] private bool useScale = true; 
		[SerializeField] private AnimationCurve scaleCurveIn = AnimationCurve.EaseInOut(0,0,1,1); 
		[SerializeField] private AnimationCurve scaleCurveOut = AnimationCurve.EaseInOut(0,0,1,1); 
		
		[SerializeField] private bool useAlpha = true;
		[SerializeField] private AnimationCurve alphaCurveIn = AnimationCurve.EaseInOut(0,0,1,1);
		[SerializeField] private AnimationCurve alphaCurveOut = AnimationCurve.EaseInOut(0,0,1,1);

		[SerializeField] private bool useDisplacement = true; 
		[SerializeField] private AnimationCurve displacementCurveIn = AnimationCurve.EaseInOut(0,0,1,1);
		[SerializeField] private AnimationCurve displacementCurveOut = AnimationCurve.EaseInOut(0,0,1,1);

		[SerializeField] private DisplacementMode displacementMode;
		[SerializeField] private Vector2 displacement = Vector2.down;
		
		private Vector2 startAnchoredPosition;

		private CancellationTokenSource cts;

		private enum DisplacementMode
		{
			Fixed,
			MultiplyBySize,
			MultiplyByParentSize
		}

		private void Awake()
		{
			startAnchoredPosition = RT.anchoredPosition;
			//canvasGroup = GetComponent<CanvasGroup>();
		}

		[Button]
		public UniTask Open()
		{
			List<Tween> tweens = new List<Tween>();
			if (useScale)
			{
				tweens.Add(RT.transform.ScaleTo(Vector3.one, duration, scaleCurveIn));
			}
			if (useAlpha)
			{
				tweens.Add(CanvasGroup.FadeIn(duration, alphaCurveIn));
			}
			if (useDisplacement)
			{
				tweens.Add(RT.AnchorTo(startAnchoredPosition, duration, displacementCurveIn));
			}
			return Play(tweens);
		}

		private UniTask Play(List<Tween> tweens)
		{
			cts?.Cancel();
			cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
			return UniTask.WhenAll(tweens.ConvertAll(x => x.Play(cts.Token)));
		}

		[Button]
		public UniTask Close()
		{
			List<Tween> tweens = new List<Tween>();
			if (useScale)
			{
				tweens.Add(RT.transform.ScaleTo(Vector3.zero, duration, scaleCurveOut));
			}
			if (useAlpha)
			{
				tweens.Add(CanvasGroup.FadeOut(duration, alphaCurveOut));
			}
			if (useDisplacement)
			{
				tweens.Add(RT.AnchorTo(GetDisplacement(), duration, displacementCurveOut));
			}
			return Play(tweens);
		}

		[Button]
		public void Show()
		{
			if (useScale)
			{
				RT.transform.localScale = Vector3.LerpUnclamped(RT.transform.localScale, Vector3.one, scaleCurveIn.Evaluate(1));
			}
			if (useAlpha)
			{
				CanvasGroup.alpha = Mathf.LerpUnclamped(CanvasGroup.alpha, 1, alphaCurveIn.Evaluate(1));
			}
			if (useDisplacement)
			{
				RT.anchoredPosition = Vector3.LerpUnclamped(RT.anchoredPosition, startAnchoredPosition, displacementCurveIn.Evaluate(1));
			}
		}
		
		[Button]
		public void Hide()
		{
			if (useScale)
			{
				RT.transform.localScale = Vector3.LerpUnclamped(RT.transform.localScale, Vector3.zero, scaleCurveOut.Evaluate(1));
			}
			if (useAlpha)
			{
				CanvasGroup.alpha = Mathf.LerpUnclamped(CanvasGroup.alpha, 0, alphaCurveOut.Evaluate(1));
			}
			if (useDisplacement)
			{
				RT.anchoredPosition = Vector3.LerpUnclamped(RT.anchoredPosition, GetDisplacement(), displacementCurveOut.Evaluate(1));
			}
		}
		
		private Vector3 GetDisplacement()
		{
			switch (displacementMode)
			{
				case DisplacementMode.Fixed:
					return displacement;
				case DisplacementMode.MultiplyBySize:
					return Vector2.Scale(displacement, RT.rect.size);
				case DisplacementMode.MultiplyByParentSize:
					return Vector2.Scale(displacement, ((RectTransform)transform.parent).rect.size);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
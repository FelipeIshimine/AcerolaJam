using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Views
{
	[RequireComponent(typeof(CanvasGroup))]
	public class Panel : MonoBehaviour
	{
		private CanvasGroup canvasGroup;
		
		[SerializeField] private float openDuration = .25f;
		[SerializeField] private float closeDuration = .25f;

		[SerializeField] private bool useScale;
		[SerializeField] private AnimationCurve openScaleCurve = AnimationCurve.EaseInOut(0,0,1,1);
		[SerializeField] private AnimationCurve closeScaleCurve = AnimationCurve.EaseInOut(0,0,1,1);
		
		[SerializeField] private bool useAlpha;
		[SerializeField] private AnimationCurve openAlphaCurve = AnimationCurve.EaseInOut(0,0,1,1);
		[SerializeField] private AnimationCurve closeAlphaCurve = AnimationCurve.EaseInOut(0,0,1,1);

		[SerializeField] private bool useScreenBorderConstraint = true;
		[SerializeField] private AnchorMode anchorMode;

		private bool isOpen = false;

		public bool IsAnimating { get; private set; }
		
		[System.Serializable]
		private enum AnchorMode
		{
			None,
			EvadeCenterStrict,
			EvadeBordersStrict,
			EvadeCenterAndBordersStrict,
		}
		
		
		private Vector2 anchorOffset;
	
		private Tween scaleTween;
		private Tween alphaTween;
		private Tween positionTween;

		private RectTransform RT => (RectTransform)transform;
		public bool IsOpen => isOpen;

		private CancellationTokenSource cts;
		
		[SerializeField] private InterruptionMode interruptionMode;
		private enum InterruptionMode
		{
			Instant,
			WaitForClose
		}
		
		public UniTask OpenAtWorldPosition(Vector3 worldPosition) => OpenAtScreenPoint(Camera.main.WorldToScreenPoint(worldPosition));

		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			//Hide();
		}

		private async UniTask Play(Tween scaleT, Tween alphaT)
		{
			cts?.Cancel();
			cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
			IsAnimating = true;
			scaleTween?.Stop();
			scaleTween = scaleT;

			alphaTween?.Stop();
			alphaTween = alphaT;

			List<UniTask> tasks = new List<UniTask>();

			if(scaleT != null) tasks.Add(scaleT.Play(cts.Token));
			if(alphaTween != null) tasks.Add(alphaTween.Play(cts.Token));

			await (tasks.Count == 0 ? UniTask.Yield(cts.Token) : UniTask.WhenAll(tasks));
			IsAnimating = false;
		}
		
		public UniTask Open() => OpenAtScreenPoint(RT.position);

		public async UniTask OpenAtScreenPoint(Vector3 screenPosition)
		{
			gameObject.SetActive(true);

			if (interruptionMode == InterruptionMode.WaitForClose && isOpen)
			{
				await Close();
			}

			transform.position = screenPosition;
			isOpen = true;
			switch (anchorMode)
			{
				case AnchorMode.EvadeCenterStrict:
					anchorOffset = Camera.main.ScreenToViewportPoint(RT.position)*2-Vector3.one;
					anchorOffset.x = Mathf.CeilToInt(Mathf.Abs(anchorOffset.x)) * Mathf.Sign(anchorOffset.x);
					anchorOffset.y = Mathf.CeilToInt(Mathf.Abs(anchorOffset.y)) * Mathf.Sign(anchorOffset.y);
					anchorOffset /= 2;
					break;
				case AnchorMode.EvadeBordersStrict:
					anchorOffset = (Camera.main.ScreenToViewportPoint(RT.position) * 2 - Vector3.one) *-1;
					
					anchorOffset.x = Mathf.CeilToInt(Mathf.Abs(anchorOffset.x)) * Mathf.Sign(anchorOffset.x);
					anchorOffset.y = Mathf.CeilToInt(Mathf.Abs(anchorOffset.y)) * Mathf.Sign(anchorOffset.y);
					anchorOffset /= 2;
					break;
				case AnchorMode.EvadeCenterAndBordersStrict:
					
					anchorOffset = Camera.main.ScreenToViewportPoint(RT.position)*2-Vector3.one;
					anchorOffset.x = Mathf.CeilToInt(Mathf.Abs(anchorOffset.x)) * Mathf.Sign(anchorOffset.x);
					anchorOffset.y = Mathf.CeilToInt(Mathf.Abs(anchorOffset.y)) * Mathf.Sign(anchorOffset.y);
					anchorOffset /= 2;
					
					Vector2 position = RT.position;
					if (position.x > Screen.currentResolution.width - RT.rect.width ||
					    position.x < RT.rect.width)
					{
						anchorOffset.x *= -1;
					}
					
					if (position.y > Screen.currentResolution.height - RT.rect.height ||
					    position.y < RT.rect.height)
					{
						anchorOffset.y *= -1;
					}
					
					break;
				case AnchorMode.None:
				default:
					anchorOffset = Vector2.zero;
					break;
			}

			RT.anchoredPosition += Vector2.Scale(RT.rect.size, anchorOffset);
			
			if (useScreenBorderConstraint)
			{
				Vector2 position = RT.position;
				position.x = Mathf.Min(position.x, Screen.currentResolution.width - RT.rect.width* .5f);
				position.x = Mathf.Max(position.x, RT.rect.width * .5f);
					
				position.y = Mathf.Min(position.y, Screen.currentResolution.height - RT.rect.height* .5f);
				position.y = Mathf.Max(position.y, RT.rect.height * .5f);

				transform.position = position;
			}

			if (!useScale)
			{
				transform.localScale = Vector3.one;
			}

			if (!useAlpha)
			{
				canvasGroup.alpha = 1;
			}
			
			await Play(
				useScale
					? transform.ScaleTo(Vector3.one, openDuration, openScaleCurve).SetTime(Tween.TimeType.Real)
					: null,
				useAlpha ? canvasGroup.FadeIn(openDuration, openAlphaCurve).SetTime(Tween.TimeType.Real) : null);
		}

		public async UniTask Close()
		{
			isOpen = false;
			await Play(
				useScale
					? transform.ScaleTo(Vector3.zero, closeDuration, closeScaleCurve).SetTime(Tween.TimeType.Real)
					: null,
				useAlpha ? canvasGroup.FadeOut(closeDuration, closeAlphaCurve).SetTime(Tween.TimeType.Real) : null
			);
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			transform.localScale = Vector3.one;
			canvasGroup.alpha = 1;
		}

		public void Hide()
		{
			gameObject.SetActive(false);
			transform.localScale = Vector3.zero;
			canvasGroup.alpha = 0;
		}
	}
	
	
}
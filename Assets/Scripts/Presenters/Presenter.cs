using System;
using UnityEngine;

namespace Presenters
{
	public class Presenter<T> : MonoBehaviour where T : Component
	{
		protected T Controller { get; private set; }
		private void Awake()
		{
			Controller = GetComponent<T>();
		}
	}
}
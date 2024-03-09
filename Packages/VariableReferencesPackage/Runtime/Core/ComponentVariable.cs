using UnityEngine;

namespace VariableReferences
{
	public class ComponentVariable<T> : MonoBehaviour, IVariableReference<T>
	{
		[field:SerializeField] public T Value { get; set; }
	}
}
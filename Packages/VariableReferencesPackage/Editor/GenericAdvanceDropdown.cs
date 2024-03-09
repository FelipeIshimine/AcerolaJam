using System;
using UnityEditor.IMGUI.Controls;

namespace VariableReferences.Editor
{
	public class GenericAdvanceDropdown : AdvancedDropdown
	{
		private readonly string[] labels;
		private readonly Action<int> callback;

		public GenericAdvanceDropdown(string[] labels, Action<int> callback) : base(new AdvancedDropdownState())
		{
			this.labels = labels;
			this.callback = callback;
		}

		protected override AdvancedDropdownItem BuildRoot()
		{
			AdvancedDropdownItem root = new AdvancedDropdownItem("Select Type");
			


			for (int i = 0; i < labels.Length; i++)
			{
				root.AddChild(new AdvancedDropdownItem(labels[i])
				{
					id = i
				});
			}
			

			return root;
		}

		protected override void ItemSelected(AdvancedDropdownItem item) => callback?.Invoke(item.id);
	}
}
using System;

namespace VariableReferences.Attributes
{
	public class DropdownPathAttribute : Attribute
	{
		public readonly string Path;

		public DropdownPathAttribute(string path)
		{
			Path = path;
		}
	}
}
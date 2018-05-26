namespace Xwrap
{
	using System;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class TemplateIdAttribute : Attribute
	{
		public Guid TemplateId { get; }
		public TemplateIdAttribute(string templateId)
		{
			if (Guid.TryParse(templateId, out var id))
			{
				this.TemplateId = id;
			}
			else
			{
				throw new ArgumentException($"Given string '{templateId}' is not a GUID.");
			}
		}
	}
}
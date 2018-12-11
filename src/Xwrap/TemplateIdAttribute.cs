namespace Xwrap
{
	using System;

	/// <summary>
	/// Attribute for marking <see cref="ItemWrapper"/> types with compatible Sitecore item templates.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class TemplateIdAttribute : Attribute
	{
		/// <summary>
		/// Gets the template ID.
		/// </summary>
		public Guid TemplateId { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateIdAttribute"/> class.
		/// </summary>
		/// <param name="templateId">The template identifier.</param>
		/// <exception cref="ArgumentException">Given string '{templateId}</exception>
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
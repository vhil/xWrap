namespace Xwrap.FieldWrappers.Abstractions
{
	using System;

	/// <summary>
	/// Field wrapper abstraction for 'general link' Sitecore field types. Implements <see cref="ILinkFieldWrapper{string}"/>
	/// </summary>
	public interface IGeneralLinkFieldWrapper : ILinkFieldWrapper<string>
    {
		/// <summary>
		/// Gets the linked item ID.
		/// </summary>
		Guid ItemId { get; }

		/// <summary>
		/// Gets the link alternate text.
		/// </summary>
		string AlternateText { get; }

		/// <summary>
		/// Gets the link description.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets a value indicating whether this link is internal.
		/// </summary>
		bool IsInternal { get; }

		/// <summary>
		/// Gets a value indicating whether this link is a media link.
		/// </summary>
		bool IsMediaLink { get; }

		/// <summary>
		/// Gets the link styles.
		/// </summary>
		string Styles { get; }

		/// <summary>
		/// Gets the _target link property.
		/// </summary>
		string Target { get; }
    }
}

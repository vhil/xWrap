namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using Sitecore.Data.Items;

	/// <summary>
	/// Field wrapper abstraction for item reference link Sitecore field types .e.g. 'droplink' Implements <see cref="ILinkFieldWrapper{Guid}"/>
	/// </summary>
	public interface ILinkFieldWrapper : ILinkFieldWrapper<Guid>
    {
    }

	/// <summary>
	/// Field wrapper abstraction for item reference link Sitecore field types .e.g. 'droplink' Implements <see cref="IFieldWrapper{TReturnrType}"/>
	/// </summary>
	public interface ILinkFieldWrapper<out TReturnType> : IFieldWrapper<TReturnType>
    {
	    /// <summary>
	    /// Gets the selected item ID.
	    /// </summary>
	   Guid ItemId { get; }

		/// <summary>
		/// Gets the selected item URL.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets the target linked item.
		/// </summary>
		/// <returns>Instance of <see cref="Item"/></returns>
		Item GetTarget();

	    /// <summary>
	    /// Wraps the target linked Sitecore item and returns an xWrap strongly typed item wrapper.
	    /// Returns null in case source item template does not match the target template ID.
	    /// </summary>
	    /// <typeparam name="TItemWrapper">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper;
    }
}

namespace Xwrap.FieldWrappers.Abstractions
{
	using System;
	using Sitecore.Data.Items;

	/// <summary>
	/// Field wrapper abstraction for 'internal link' Sitecore field types. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	public interface IInternalLinkFieldWrapper : IFieldWrapper<string>
	{
		/// <summary>
		/// Gets the target item ID.
		/// </summary>
		Guid TargetId { get; }

		/// <summary>
		/// Gets the target item path.
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Gets the target item URL.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets the target linked item.
		/// </summary>
		/// <returns></returns>
		Item GetTarget();

		/// <summary>
		/// Wraps the target linked Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case source item template does not match the target template ID.
		/// </summary>
		/// <typeparam name="TItemWrapper">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper;
	}
}

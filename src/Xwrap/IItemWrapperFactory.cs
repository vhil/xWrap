namespace Xwrap
{
	using System;
	using Exceptions;
	using System.Collections.Generic;
	using Sitecore.Data.Items;

	public interface IItemWrapperFactory
	{
		/// <summary>
		/// Wraps Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Throws exception in case source item template does not match the target template ID.
		/// </summary>
		/// <param name="item">Item to wrap</param>
		/// <exception cref="ItemWrappingException">if the source item template does not match the target template ID</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		TItemWrapper WrapItem<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper;

		/// <summary>
		/// Wraps Sitecore items and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="items">Items to wrap</param>
		IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(IEnumerable<Item> items) where TItemWrapper : ItemWrapper;

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		IEnumerable<TItemWrapper> GetChildren<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper;

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		IEnumerable<TItemWrapper> GetChildren<TItemWrapper>(ItemWrapper item) where TItemWrapper : ItemWrapper;

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		IEnumerable<TItemWrapper> GetChildrenReccursively<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper;

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		IEnumerable<TItemWrapper> GetChildrenReccursively<TItemWrapper>(ItemWrapper item) where TItemWrapper : ItemWrapper;
	}
}

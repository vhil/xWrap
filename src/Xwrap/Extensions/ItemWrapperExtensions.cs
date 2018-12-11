namespace Xwrap.Extensions
{
	using System;
	using System.Collections.Generic;
	using Sitecore.Data.Items;
	using Exceptions;

	/// <summary>
	/// Extensions methods for wrapping items into xWrap based item wrappers.
	/// </summary>
	public static class ItemWrapperExtensions
	{
		private static IItemWrapperFactory Factory => ItemWrapperFactory.Instance;

		internal static TemplateIdAttribute GetTemplateIdAttribute(this Type itemWrapperType)
		{
			return Attribute.GetCustomAttribute(itemWrapperType, typeof(TemplateIdAttribute)) as TemplateIdAttribute;
		}

		internal static TemplateIdAttribute GetTemplateIdAttribute(this ItemWrapper itemWrapper)
		{
			return GetTemplateIdAttribute(itemWrapper.GetType());
		}

		/// <summary>
		/// Wraps Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case the source item template does not match the target template ID.
		/// </summary>
		/// <param name="item">Item to wrap</param>
		/// <exception cref="ItemWrappingException">if the source item template does not match the target template ID</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public static TItemWrapper WrapItem<TItemWrapper>(this Item item) 
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapItem<TItemWrapper>(item);
		}

		/// <summary>
		/// Wraps Sitecore items and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="items">Items to wrap</param>
		public static IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(this IEnumerable<Item> items) 
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapItems<TItemWrapper>(items);
		}

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		public static IEnumerable<TItemWrapper> WrapChildren<TItemWrapper>(this Item item)
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapChildren<TItemWrapper>(item);
		}

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		public static IEnumerable<TItemWrapper> WrapChildren<TItemWrapper>(this ItemWrapper item)
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapChildren<TItemWrapper>(item);
		}

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		public static IEnumerable<TItemWrapper> WrapChildrenReccursively<TItemWrapper>(this Item item)
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapChildrenReccursively<TItemWrapper>(item);
		}

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		public static IEnumerable<TItemWrapper> WrapChildrenReccursively<TItemWrapper>(this ItemWrapper item)
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapChildrenReccursively<TItemWrapper>(item);
		}
	}
}
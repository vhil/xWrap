namespace Xwrap
{
	using System;
	using System.Collections.Generic;
	using Sitecore.Configuration;
	using Sitecore.Data.Items;
	using Extensions;
	using System.Linq;
	using Exceptions;

	/// <summary>
	/// A factory type for creating ItemWrapper instances.
	/// </summary>
	public class ItemWrapperFactory : IItemWrapperFactory
	{
		/// <summary>
		/// Configured instance of the IItemWrapperFactory in configuration by path "xWrap/itemWrapperFactory"
		/// </summary>
		public static IItemWrapperFactory Instance => Factory.CreateObject("xWrap/itemWrapperFactory", true) as IItemWrapperFactory;

		/// <summary>
		/// Wraps Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case the source item template does not match the target template ID.
		/// </summary>
		/// <param name="item">Item to wrap</param>
		/// <exception cref="ItemWrappingException">if the source item template does not match the target template ID</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual TItemWrapper WrapItem<TItemWrapper>(Item item) 
			where TItemWrapper : ItemWrapper
		{
			if (item == null) return default(TItemWrapper);

			var templateIdAttr = typeof(TItemWrapper).GetTemplateIdAttribute();

			if (templateIdAttr == null || item.IsDerived(templateIdAttr.TemplateId))
			{
				return Activator.CreateInstance(typeof(TItemWrapper), item) as TItemWrapper;
			}

			return default(TItemWrapper);
		}

		/// <summary>
		/// Wraps Sitecore items and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="items">Items to wrap</param>
		public virtual IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(IEnumerable<Item> items) 
			where TItemWrapper : ItemWrapper
		{
			if (items == null) return Enumerable.Empty<TItemWrapper>();

			return items.Select(this.WrapItem<TItemWrapper>).Where(x => x != null);
		}

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		public virtual IEnumerable<TItemWrapper> WrapChildren<TItemWrapper>(Item item) 
			where TItemWrapper : ItemWrapper
		{
			return this.WrapItems<TItemWrapper>(item.Children);
		}

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		public virtual IEnumerable<TItemWrapper> WrapChildren<TItemWrapper>(ItemWrapper item) 
			where TItemWrapper : ItemWrapper
		{
			return this.WrapItems<TItemWrapper>(item.OriginalItem.Children);
		}

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item to get children from</param>
		public virtual IEnumerable<TItemWrapper> WrapChildrenReccursively<TItemWrapper>(Item item) 
			where TItemWrapper : ItemWrapper
		{
			var templateIdAttr = typeof(TItemWrapper).GetTemplateIdAttribute();

			return this.WrapItems<TItemWrapper>(templateIdAttr != null 
				? item.GetChildrenReccursively(templateIdAttr.TemplateId) 
				: item.GetChildrenReccursively());
		}

		/// <summary>
		/// Gets children of an item reccursively and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <param name="item">Item wrapper to get children from</param>
		public virtual IEnumerable<TItemWrapper> WrapChildrenReccursively<TItemWrapper>(ItemWrapper item) 
			where TItemWrapper : ItemWrapper
		{
			return this.WrapChildrenReccursively<TItemWrapper>(item.OriginalItem);
		}
	}
}

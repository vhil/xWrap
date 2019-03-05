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

		/// <summary>
		/// Finds first parent item of requested template and wraps it into xWrap strongly typed item wrapper.
		/// Returns null in case if there are no parent items inherited from requested template.
		/// </summary>
		/// <typeparam name="TItemWrapper"></typeparam>
		/// <param name="item">item</param>
		/// <returns></returns>
		public TItemWrapper WrapFirstParent<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper
		{
			var templateIdAttr = typeof(TItemWrapper).GetTemplateIdAttribute();

			foreach (var ancestor in item.Axes.GetAncestors().Reverse())
			{
				if (templateIdAttr == null || ancestor.IsDerived(templateIdAttr.TemplateId))
				{
					return this.WrapItem<TItemWrapper>(ancestor);
				}
			}

			return default(TItemWrapper);
		}
	}
}

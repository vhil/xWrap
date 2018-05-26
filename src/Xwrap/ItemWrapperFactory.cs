namespace Xwrap
{
	using System;
	using System.Collections.Generic;
	using Sitecore.Configuration;
	using Sitecore.Data.Items;
	using Extensions;
	using System.Linq;

	public class ItemWrapperFactory : IItemWrapperFactory
	{
		public static IItemWrapperFactory Instance => Factory.CreateObject("xWrap/itemWrapperFactory", true) as IItemWrapperFactory;

		public TItemWrapper WrapItem<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper
		{
			var templateIdAttr = typeof(TItemWrapper).GetTemplateIdAttribute();

			if (templateIdAttr == null || item.IsDerived(templateIdAttr.TemplateId))
			{
				return Activator.CreateInstance(typeof(TItemWrapper), item) as TItemWrapper;
			}

			return default(TItemWrapper);
		}

		public IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(IEnumerable<Item> items) where TItemWrapper : ItemWrapper
		{
			return items.Select(this.WrapItem<TItemWrapper>).Where(x => x != null);
		}
	}
}

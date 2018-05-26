namespace Xwrap.Extensions
{
	using System;
	using System.Collections.Generic;
	using Sitecore.Data.Items;

	public static class ItemWrapperExtensions
	{
		private static IItemWrapperFactory Factory => ItemWrapperFactory.Instance;

		public static TemplateIdAttribute GetTemplateIdAttribute(this Type itemWrapperType)
		{
			return Attribute.GetCustomAttribute(itemWrapperType, typeof(TemplateIdAttribute)) as TemplateIdAttribute;
		}

		public static TemplateIdAttribute GetTemplateIdAttribute(this ItemWrapper itemWrapper)
		{
			return GetTemplateIdAttribute(itemWrapper.GetType());
		}

		public static TItemWrapper WrapItem<TItemWrapper>(this Item item) 
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapItem<TItemWrapper>(item);
		}

		public static IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(this IEnumerable<Item> items) 
			where TItemWrapper : ItemWrapper
		{
			return Factory.WrapItems<TItemWrapper>(items);
		}
	}
}
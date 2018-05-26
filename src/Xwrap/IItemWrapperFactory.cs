namespace Xwrap
{
	using System.Collections.Generic;
	using Sitecore.Data.Items;

	public interface IItemWrapperFactory
	{
		TItemWrapper WrapItem<TItemWrapper>(Item item) where TItemWrapper : ItemWrapper;
		IEnumerable<TItemWrapper> WrapItems<TItemWrapper>(IEnumerable<Item> items) where TItemWrapper : ItemWrapper;
	}
}

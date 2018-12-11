namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

	/// <summary>
	/// Field wrapper abstraction for list Sitecore field types such as 'treelist' or 'multilist'. Implements <see cref="IFieldWrapper{IEnumerable{Guid}}"/>
	/// </summary>
	public interface IListFieldWrapper : IFieldWrapper<IEnumerable<Guid>>, IEnumerable<Item>
    {
		/// <summary>
		/// Gets the list of selected items.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Item> GetItems();

	    /// <summary>
	    /// Wraps Sitecore items and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
	    /// Items which are not inherited from target template are being skipped and not included into result.
	    /// </summary>
		IEnumerable<TItemWrapper> WrapItems<TItemWrapper>() where TItemWrapper : ItemWrapper;
    }
}

namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface IListFieldWrapper : IFieldWrapper<IEnumerable<Guid>>, IEnumerable<Item>
    {
        IEnumerable<Item> GetItems();
        IEnumerable<TItemWrapper> WrapItems<TItemWrapper>() where TItemWrapper : ItemWrapper;
    }
}

namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using Sitecore.Data.Items;

    public interface ILinkFieldWrapper : ILinkFieldWrapper<Guid>
    {
    }

    public interface ILinkFieldWrapper<out TReturnrType> : IFieldWrapper<TReturnrType>
    {
        string Url { get; }
        Item GetTarget();
	    TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper;
    }
}

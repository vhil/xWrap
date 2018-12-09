namespace Xwrap.FieldWrappers.Abstractions
{
	using System;
	using Sitecore.Data.Items;

	public interface IInternalLinkFieldWrapper : IFieldWrapper<string>
	{
		Guid TargetId { get; }
		string Path { get; }
		string Url { get; }
		Item GetTarget();
		TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper;
	}
}

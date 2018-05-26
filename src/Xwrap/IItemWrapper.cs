namespace Xwrap
{
	using System;
	using Sitecore.Data.Items;
	using System.Collections.Generic;

	public interface IItemWrapper
	{
		Item Item { get; }
		Guid Id { get; }
		Guid TemplateId { get; }
		string Name { get; }
		string DisplayName { get; }
		string FullPath { get; }
		IEnumerable<TItemWrapper> GetChildren<TItemWrapper>() where TItemWrapper : ItemWrapper;
		TItemWrapper GetFirstChild<TItemWrapper>() where TItemWrapper : ItemWrapper;
	}
}

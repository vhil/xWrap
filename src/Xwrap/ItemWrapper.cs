namespace Xwrap
{
	using System;
	using Sitecore.Data.Items;
	using Exceptions;
	using System.Collections.Generic;
	using Extensions;
	using System.Linq;

	public abstract class ItemWrapper : IItemWrapper
	{
		protected IItemWrapperFactory Factory => ItemWrapperFactory.Instance;

		public Item Item { get; }
		public Guid Id => this.Item.ID.Guid;
		public Guid TemplateId => this.Item.TemplateID.Guid;
		public string Name => this.Item.Name;
		public string DisplayName => this.Item.DisplayName;
		public string FullPath => this.Item.Paths.FullPath;

		protected ItemWrapper(Item item)
		{
			this.Item = item ?? throw new ItemWrappingException(
				$"Unable to wrap item. Constructor argument '{nameof(item)}' was null.");

			this.ValidateTemplate();
		}

		public IEnumerable<TItemWrapper> GetChildren<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.Factory.WrapItems<TItemWrapper>(this.Item.Children);
		}

		public TItemWrapper GetFirstChild<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.GetChildren<TItemWrapper>().FirstOrDefault();
		}

		protected virtual void ValidateTemplate()
		{
			var expectedTemplateAttr = this.GetTemplateIdAttribute();

			if (expectedTemplateAttr != null && !this.Item.IsDerived(expectedTemplateAttr.TemplateId))
			{
				throw new ItemWrappingException(
					$"Unable to wrap item '{this.FullPath}' of template {this.TemplateId}. " +
					$"'{this.GetType().Name}' item wrapper expects item of {expectedTemplateAttr.TemplateId} template ID.");
			}
		}
	}
}
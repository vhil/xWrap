﻿namespace Xwrap
{
	using System;
	using Sitecore.Data.Items;
	using Exceptions;
	using System.Collections.Generic;
	using Extensions;
	using System.Linq;
	using Sitecore.Data;
	using FieldWrappers.Abstractions;

	public abstract class ItemWrapper
	{
		protected IItemWrapperFactory ItemWrapperFactory => Xwrap.ItemWrapperFactory.Instance;
		protected IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

		public Item OriginalItem { get; }
		public Guid Id => this.OriginalItem.ID.Guid;
		public Guid TemplateId => this.OriginalItem.TemplateID.Guid;
		public string Name => this.OriginalItem.Name;
		public string DisplayName => this.OriginalItem.DisplayName;
		public string FullPath => this.OriginalItem.Paths.FullPath;

		protected ItemWrapper(Item item)
		{
			this.OriginalItem = item ?? throw new ItemWrappingException(
				$"Unable to wrap item. Constructor argument '{nameof(item)}' was null.");

			this.ValidateTemplate();
		}

		public IFieldWrapper this[string fieldName] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldName);

		public IFieldWrapper this[ID fieldId] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldId);

		public IEnumerable<TItemWrapper> GetChildren<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.ItemWrapperFactory.WrapItems<TItemWrapper>(this.OriginalItem.Children);
		}

		public TItemWrapper GetFirstChild<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.GetChildren<TItemWrapper>().FirstOrDefault();
		}

		protected virtual void ValidateTemplate()
		{
			var expectedTemplateAttr = this.GetTemplateIdAttribute();

			if (expectedTemplateAttr != null && !this.OriginalItem.IsDerived(expectedTemplateAttr.TemplateId))
			{
				throw new ItemWrappingException(
					$"Unable to wrap item '{this.FullPath}' of template {this.TemplateId}. " +
					$"'{this.GetType().Name}' item wrapper expects item of {expectedTemplateAttr.TemplateId} template ID.");
			}
		}
	}
}